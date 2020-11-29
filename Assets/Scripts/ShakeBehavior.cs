using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeBehavior : MonoBehaviour
{
    public CinemachineImpulseDefinition impulseDefinition;

    private void OnValidate()
        {
            impulseDefinition.OnValidate();
        }

        /// <summary>Broadcast the Impulse Signal onto the appropriate channels,
        /// using a custom position and impact velocity</summary>
        /// <param name="position">The world-space position from which the impulse will emanate</param>
        /// <param name="velocity">The impact magnitude and direction</param>
        public void GenerateImpulseAt(Vector3 position, Vector3 velocity)
        {
            if (impulseDefinition != null) {
                impulseDefinition.CreateEvent(position, velocity);
            }
                
        }

        /// <summary>Broadcast the Impulse Signal onto the appropriate channels, using
        /// a custom impact velocity, and this transfom's position.</summary>
        /// <param name="velocity">The impact magnitude and direction</param>
        public void GenerateImpulse(Vector3 velocity)
        {
            GenerateImpulseAt(transform.position, velocity);
        }

        /// <summary>Broadcast the Impulse Signal onto the appropriate channels, using
        /// a custom impact force, with the standard direction, and this transfom's position.</summary>
        /// <param name="force">The impact magnitude.  1 is normal</param>
        public void GenerateImpulse(float force)
        {
            GenerateImpulseAt(transform.position, new Vector3(0, -force, 0));
        }

        /// <summary>Broadcast the Impulse Signal onto the appropriate channels, 
        /// with default velocity = (0, -1, 0), and a default position which is
        /// this transform's location.</summary>
        public void GenerateImpulse()
        {
            GenerateImpulse(Vector3.down);
        }
}

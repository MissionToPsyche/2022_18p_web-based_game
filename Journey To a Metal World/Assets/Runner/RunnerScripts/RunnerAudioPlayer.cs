using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAudioPlayer : MonoBehaviour
{
    [Header("Collisisons")]
    [SerializeField] AudioClip collision_clip;
    [SerializeField] [Range (0f, 1f)] float collision_volume;

    public void PlayCollisionClip()
    {
        if (null != this.collision_clip )
        {
            AudioSource.PlayClipAtPoint(this.collision_clip, Camera.main.transform.position, this.collision_volume);
        }
    }
}
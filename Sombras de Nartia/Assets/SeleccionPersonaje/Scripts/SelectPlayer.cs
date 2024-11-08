using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NuevoPersonaje", menuName="Personaje")]
public class SelectPlayer : ScriptableObject
{
    public GameObject player;
    public Sprite imagen;
    public string nombre;
}

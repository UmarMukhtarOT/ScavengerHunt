using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MatchScriptable", menuName = "ScriptableObjects/MatchScriptable")]
public class MatchSO : ScriptableObject
{
    [Serializable]
   public class ItemProps 
   { 
        public string name;
        public int count;
        public Sprite IconSpr;
        
    
   }



    public List<ItemProps> AllItems;



}

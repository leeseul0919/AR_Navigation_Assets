﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using System.Collections.Generic;

[BsonIgnoreExtraElements]
public class TestData
{
    public static int COLLECTION_MAX_COUNT = 4;
    public int ID;
    public string Text;
    public float Node_X;
    public float Node_Y;
    public int floor;
    
    /*
     * NOTE: If key or value type of Dictionary is a value type it is needed to specify the serializer. Same goes for other generic types, like List, etc...
     * This happens because of the AOT nature of C++ and how reflections work in C#. For more info see https://docs.unity3d.com/Manual/ScriptingRestrictions.html
    [BsonSerializer(typeof(DictionaryInterfaceImplementerSerializer<Dictionary<int, DictionaryData>>)), BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)] //default dictionary representation is only supported when keys are strings
    public Dictionary<int, DictionaryData> intDictionary;
    [BsonSerializer(typeof(DictionaryInterfaceImplementerSerializer<Dictionary<string, int>>))]
    public Dictionary<string, int> intValuedictionary;
    */

    //NOTE: Using other constructors for MongoDB failed for me, probably because of an issue with reflections
    public TestData() { } //needed for MongoDB/BsonSerialization

    public static TestData Random(string nameValue)
    {
        var listSize = UnityEngine.Random.Range(1, COLLECTION_MAX_COUNT);
        var randomList = new List<ListData>(listSize);
        for (var i = 0; i < listSize; i++)
        {
            randomList.Add(ListData.Random());
        }

        var dictSize = UnityEngine.Random.Range(1, COLLECTION_MAX_COUNT);
        var randomDict = new Dictionary<string, DictionaryData>(dictSize);
        for (var i = 0; i < dictSize; i++)
        {
            randomDict.Add($"{nameValue}_{i}", DictionaryData.Random());
        }

        return new TestData()
        {
            ID = 0,
            Text = nameValue,
            Node_X = 0,
            Node_Y = 0,
        };
    }

    public class ListData
    {
        public float value;

        public static ListData Random() => new ListData() { value = UnityEngine.Random.value };
    }

    public class DictionaryData
    {
        public int value;

        public static DictionaryData Random() => new DictionaryData() { value = UnityEngine.Random.Range(int.MinValue, int.MaxValue) };
    }
}
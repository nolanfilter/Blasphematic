using UnityEngine;
using System.Collections;

public class DictionaryManager : MonoBehaviour {

    public TextAsset dictionaryTextAsset = null;

    private string[] adjectiveDictionary = new string[ 0 ];
    private string[] nounDictionary = new string[ 0 ];

    private static DictionaryManager internalInstance;
    public static DictionaryManager instance
    {
        get
        {
            return internalInstance;
        }
    }

    void Awake()
    {
        if( internalInstance != null )
        {
            Debug.LogError( "Only one instance of DictionaryManager allowed. Destroying " + gameObject );
            Destroy( gameObject );
            return;
        }

        internalInstance = this;

        LoadDictionary();
    }

    private void LoadDictionary()
    {
        if( dictionaryTextAsset == null )
            return;

        string[] components = dictionaryTextAsset.text.Split( '\n' );

        if( components.Length > 0 )
        {
            adjectiveDictionary = components[ 0 ].Split( ',' );
        }

        if( components.Length > 1 )
        {
            nounDictionary = components[ 1 ].Split( ',' );
        }
    }

    public static string GetNextAdjective()
    {
        if( instance )
            return instance.internalGetNextAdjective();

        return "";
    }

    private string internalGetNextAdjective()
    {
        if( adjectiveDictionary.Length == 0 )
            return "";

        return adjectiveDictionary[ Random.Range( 0, adjectiveDictionary.Length ) ];
    }
       
    public static string GetNextNoun()
    {
        if( instance )
            return instance.internalGetNextNoun();

        return "";
    }

    private string internalGetNextNoun()
    {
        if( nounDictionary.Length == 0 )
            return "";

        return nounDictionary[ Random.Range( 0, nounDictionary.Length ) ];
    }
}

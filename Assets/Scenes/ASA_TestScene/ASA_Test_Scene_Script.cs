using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASA_Test_Scene_Script : MonoBehaviour
{
    /// <summary>
    /// The Anchor Module script that handles to usage of ASA.
    /// </summary>
    [SerializeField]
    [Tooltip("The Anchor Module script that handles to usage of ASA.")]
    public AnchorModuleScript anchorModuleScript;

    /// <summary>
    /// The Object, which position is stored in ASA and that is repositioned when a old position is loaded.
    /// </summary>
    [SerializeField]
    [Tooltip("The Object, which position is stored in ASA and that is repositioned when a old position is loaded.")]
    public GameObject objectToSavePositionOf;

    /// <summary>
    /// The Account Id of the ASA instance.
    /// </summary>
    [SerializeField]
    [Tooltip("The Account Id of the ASA instance.")]
    public string spatialAnchorsAccountId;

    /// <summary>
    /// The Account Key of the ASA instance.
    /// </summary>
    [SerializeField]
    [Tooltip("The Account key of the ASA instance.")]
    public string spatialAnchorsAccountKey;

    /// <summary>
    /// The Account Domain of the ASA instance.
    /// </summary>
    [SerializeField]
    [Tooltip("The Account Domain of the ASA instance.")]
    public string spatialAnchorsAccountDomain;

    /// <summary>
    /// The variable that stores the identifier of a created anchor, so that the last created anchor can be loaded again.
    /// </summary>
    private string anchorIdentifier = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void SavePosition()
    {
        await anchorModuleScript.StartAzureSession(spatialAnchorsAccountId, spatialAnchorsAccountKey, spatialAnchorsAccountDomain);
        Debug.Log("Session Started");
        anchorIdentifier = await anchorModuleScript.CreateAzureAnchor(objectToSavePositionOf, DateTimeOffset.Now.AddDays(7));
        //anchorModuleScript.StopAzureSession();
    }

    public async void LoadPosition()
    {
        await anchorModuleScript.StartAzureSession(spatialAnchorsAccountId, spatialAnchorsAccountKey, spatialAnchorsAccountDomain);
        anchorModuleScript.FindAzureAnchor(anchorIdentifier, objectToSavePositionOf);
        //anchorModuleScript.StopAzureSession();
    }
}

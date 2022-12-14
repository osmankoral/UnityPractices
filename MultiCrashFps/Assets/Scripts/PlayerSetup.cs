using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour 
{

	[SerializeField]
	Behaviour[] componenetsToDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	Camera sceneCamera;

	void Start () 
	{
		if (!isLocalPlayer) 
		{
			DisableComponents ();
			AssingRemoteLayer ();

		} else 
		{
			sceneCamera = Camera.main;

			if (sceneCamera != null) 
			{
				sceneCamera.gameObject.SetActive (false);
			}
		}

		GetComponent<Player> ().Setup ();
	}
		
	public override void OnStartClient()
	{
		base.OnStartClient ();

		string _netID = GetComponent<NetworkIdentity> ().netId.ToString();
		Player _player = GetComponent<Player> ();

		GameManager.RegisterPlayer (_netID, _player);
	}

	void AssingRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	void DisableComponents()
	{
		for (int i = 0; i < componenetsToDisable.Length; i++) 
		{
			componenetsToDisable [i].enabled = false;
		}
	}

	void OnDisable()
	{
		if (sceneCamera != null) 
		{
			sceneCamera.gameObject.SetActive (true);
		}

		GameManager.UnRegisterPlayer (transform.name);
	}
}



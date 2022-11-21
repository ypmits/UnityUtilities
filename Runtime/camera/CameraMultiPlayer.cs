using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
<summary>
All Camera-multiplayer-related stuff goes in here.
Make sure
</summary>
*/
namespace ypmits.unitytools
{
	public class CameraMultiplayer : MonoBehaviour
	{
		private static Camera _camera;
		private static CameraMultiplayer _instance;
		private static List<int> _connectedPlayers = new List<int>();
		private static int _maxConnectedPlayers = 4;

		public static CameraMultiplayer instance
		{
			get
			{
				if (Equals(_instance, null))
				{
					_instance = FindObjectOfType(typeof(CameraMultiplayer)) as CameraMultiplayer;
					_camera = CameraMultiplayer.instance.gameObject.GetComponent<Camera>();

					if (Equals(_instance, null)) throw new UnityException("CameraMultiplayer does not exist.");
				}
				return _instance;
			}
		}

		/**
		<summary>
		Set the bounds of the camera according to the playerID:
		</summary>
		*/
		public void AddPlayer(int rewiredPlayerID)
		{
			if (_connectedPlayers.Count == _maxConnectedPlayers) throw new UnityException("You can't add more than 4 players to this camera-system.");
			_connectedPlayers.Add(rewiredPlayerID);
			RedrawSystem();
		}

		public void AddPlayers(int[] rewiredPlayerIDs)
		{
			// AddPlayer(rewiredPlayerIDs.toList());
		}

		public void AddPlayers(List<int> rewiredPlayerIDs)
		{
			RedrawSystem();
		}

		public void ClearAllPlayers()
		{
			// _connectedPlayers
		}

		/**
		<summary>
		Set the bounds of the camera according to the playerID:
		</summary>
		*/
		private void RedrawSystem()
		{
			// Determine the sizes of the camera-rect for each player:
			switch (_connectedPlayers.Count)
			{
				case 1:
					_camera.rect = new Rect(0, 0, 1, 1);
					break;

				case 2:
					// Vertical:
					if (_connectedPlayers[0] == 1)
						_camera.rect = new Rect(0, 0, .5f, 1);
					else if (_connectedPlayers[1] == 2)
						_camera.rect = new Rect(.5f, 0, .5f, 1);
					break;

				case 3:
					if (_connectedPlayers[0] == 1)
						_camera.rect = new Rect(0, 0, .5f, .5f);
					else if (_connectedPlayers[1] == 2)
						_camera.rect = new Rect(.5f, 0, .5f, .5f);
					else if (_connectedPlayers[2] == 3)
						_camera.rect = new Rect(0, .5f, .5f, .5f);
					break;

				case 4:
					if (_connectedPlayers[0] == 1)
						_camera.rect = new Rect(0, 0, .5f, .5f);
					else if (_connectedPlayers[1] == 2)
						_camera.rect = new Rect(.5f, 0, .5f, .5f);
					else if (_connectedPlayers[2] == 3)
						_camera.rect = new Rect(0, .5f, .5f, .5f);
					else if (_connectedPlayers[3] == 3)
						_camera.rect = new Rect(.5f, .5f, .5f, .5f);
					break;
			}
		}
	}
}

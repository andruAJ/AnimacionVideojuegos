using UnityEngine;

public class Game: MonoBehaviour
{

    #region Singleton
    private static Game instance;



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateGame() 
    {
        GameObject game = new GameObject("Game");
        instance = game.AddComponent<Game>();
        DontDestroyOnLoad(game);
    }

    public static Game Instance 
    {
        get 
        {
            if (instance == null) 
            {
                CreateGame();
            }
            return instance;
        }
    }
    #endregion

    private CharacterState playerOne;

    public CharacterState PlayerOne => playerOne;

    private void CreatePlayer() 
    { 
        GameObject playerOneGO = new GameObject("PlayerOne");
        playerOne = playerOneGO.AddComponent<CharacterState>();
        DontDestroyOnLoad(playerOneGO);
    }
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            CreatePlayer();
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }
}

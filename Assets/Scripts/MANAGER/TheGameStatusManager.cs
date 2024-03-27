


public class TheGameStatusManager
{

    public enum GAME_STATUS
    {
        Loading,
        Playing,
        Pausing,
        Victory,
        Gameover,
    }

    public static GAME_STATUS CURRENT_STATUS;



    public static void SetGameStatus(GAME_STATUS eGameStatus)
    {

        switch (eGameStatus)
        {
            case GAME_STATUS.Loading:
                break;
            case GAME_STATUS.Playing:
                break;
            case GAME_STATUS.Pausing:
                // ThePopupManager.Instance.Show(ThePopupManager.POP_UP.);
                break;
            case GAME_STATUS.Victory:

                if (CURRENT_STATUS == GAME_STATUS.Playing)
                    ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Victory, 2f);
                break;
            case GAME_STATUS.Gameover:
                if (CURRENT_STATUS == GAME_STATUS.Playing)
                    ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Gameover, 2f);
                break;

        }
        CURRENT_STATUS = eGameStatus;
    }






    public static bool CurrentStatus(GAME_STATUS eGameStatus)
    {
        return CURRENT_STATUS == eGameStatus;
    }

}

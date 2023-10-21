using UnityEngine;

public class Utility
{
    public static Vector2 GetInitPos(PlayerColor color)
    {
        switch (color)
        {
            case PlayerColor.White:
                return new Vector2(-6, 5);
            case PlayerColor.Black:
                return new Vector2(6, 5);
            case PlayerColor.Blue:
                return new Vector2(-6, -5);
            case PlayerColor.Red:
                return new Vector2(6, -5);
        }
        return Vector2.zero;
    }
    public static PlayerColor GetPlayerColor(int index)
    {
        if (index == 0)
            return PlayerColor.White;
        if (index == 1)
            return PlayerColor.Black;
        if (index == 2)
            return PlayerColor.Blue;
        return PlayerColor.Red;
    }


}

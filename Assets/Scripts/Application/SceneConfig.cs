using System.Collections;
using System.Collections.Generic;

internal static class ScenesConfig
{
  
    public const string MeinMenuScene = "MainMenu";
    public const string GameOverScene = "GameOver";
    public const string LandingScene = "LandingScene";
    public const string FirstGameLevel = "GameLevel01";
    public const string SecondtGameLevel = "GameLevel02";
    public const string ThirdGameLevel   = "GameLevel03";
    public const string ForthtGameLevel  = "GameLevel04";
    public const string FifthtGameLevel  = "GameLevel05";
   

    public readonly static Dictionary<int, string> gameLevels = new Dictionary < int, string>
        {
         { 1,  FirstGameLevel},
         { 2,  SecondtGameLevel},
         { 3,  ThirdGameLevel},
         { 4,  ForthtGameLevel},
        };
        
        

}
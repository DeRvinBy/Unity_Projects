using TMPro;

namespace Scripts.Localization
{
    public class LIPlanets : LanguageInterprater
    {
        public TextMeshProUGUI MenuButton;

        public TextMeshProUGUI MenuLabel;
        public TextMeshProUGUI MenuBackButton;
        public TextMeshProUGUI MenuRestartButton;
        public TextMeshProUGUI MenuExitButton;
        public TextMeshProUGUI MenuLanguageLabel;
        public TextMeshProUGUI MenuLanguageButton;

        public TextMeshProUGUI EvacuationButton;

        public TextMeshProUGUI LoseLoseLabel;
        public TextMeshProUGUI LoseRestartButton;
        public TextMeshProUGUI LoseExitButton;

        public TextMeshProUGUI WinLabel;
        public TextMeshProUGUI WinRestartButton;
        public TextMeshProUGUI WinExitButton;

        public override void SetTranslation()
        {
            MenuButton.text = Localization.Translate.Planet_MenuButton;

            MenuLabel.text = Localization.Translate.Planet_Menu_MenuLabel;
            MenuBackButton.text = Localization.Translate.Planet_Menu_BackButton;
            MenuRestartButton.text = Localization.Translate.Planet_Menu_RestartButton;
            MenuExitButton.text = Localization.Translate.Planet_Menu_ExitButton;
            MenuLanguageLabel.text = Localization.Translate.Planet_Menu_LanguageLabel;
            MenuLanguageButton.text = Localization.Translate.Planet_Menu_LanguageButton;

            EvacuationButton.text = Localization.Translate.Planet_EvacuationButton;

            LoseLoseLabel.text = Localization.Translate.Planet_Lose_LoseLabel;
            LoseRestartButton.text = Localization.Translate.Planet_Lose_RestartButton;
            LoseExitButton.text = Localization.Translate.Planet_Lose_ExitButton;

            WinLabel.text = Localization.Translate.Planet_Win_WinLabel;
            WinRestartButton.text = Localization.Translate.Planet_Win_RestartButton;
            WinExitButton.text = Localization.Translate.Planet_Win_ExitButton;
        }
    }
}
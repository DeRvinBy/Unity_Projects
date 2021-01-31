using TMPro;

namespace Scripts.Localization
{
    public class LIMainMenu : LanguageInterprater
    {
        public TextMeshProUGUI PlayButton;
        public TextMeshProUGUI SettingsButton;

        public TextMeshProUGUI SettingsLabel;
        public TextMeshProUGUI SettingsBackButton;
        public TextMeshProUGUI SettingsExitButton;
        public TextMeshProUGUI SettingsLanguageLabel;
        public TextMeshProUGUI SettingsLanguageButton;

        public TextMeshProUGUI BuyConfrimLabel;
        public TextMeshProUGUI BuyConfrimApplyButton;
        public TextMeshProUGUI BuyConfrimCancelButton;

        public TextMeshProUGUI BuyErrorLabel;
        public TextMeshProUGUI BuyErrorCancelButton;

        public override void SetTranslation()
        {
            PlayButton.text = Localization.Translate.Menu_PlayButton;
            SettingsButton.text = Localization.Translate.Menu_SettingsButton;

            SettingsLabel.text = Localization.Translate.Menu_Settings_SettingsLabel;
            SettingsBackButton.text = Localization.Translate.Menu_Settings_BackButton;
            SettingsExitButton.text = Localization.Translate.Menu_Settings_ExitButton;
            SettingsLanguageLabel.text = Localization.Translate.Menu_Settings_LanguageLabel;
            SettingsLanguageButton.text = Localization.Translate.Menu_Settings_LanguageButton;

            BuyConfrimLabel.text = Localization.Translate.Menu_BuyConfrim_BuyConfrimLabel;
            BuyConfrimApplyButton.text = Localization.Translate.Menu_BuyConfrim_ApplyButton;
            BuyConfrimCancelButton.text = Localization.Translate.Menu_BuyConfrim_CancelButton;

            BuyErrorLabel.text = Localization.Translate.Menu_BuyError_BuyErrorLabel;
            BuyErrorCancelButton.text = Localization.Translate.Menu_BuyError_CancelButton;
        }
    }
}
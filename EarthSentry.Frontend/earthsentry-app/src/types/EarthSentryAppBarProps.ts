export type EarthSentryAppBarProps = DarkLightModeSwitchProps & {
};

export type DarkLightModeSwitchProps =
{
  darkMode: boolean;
  onToggle: () => void;
}
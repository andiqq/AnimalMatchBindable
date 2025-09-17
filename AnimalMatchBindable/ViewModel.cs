using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnimalMatchBindable;

public partial class ViewModel : ObservableObject
{
    
    private static readonly List<string> AnimalEmoji =
    [
        "🐙","🐙",
        "🐡","🐡",
        "🦧","🦧",
        "🐳","🐳",
        "🐪","🐪",
        "🦘","🦘",
        "🦔","🦔",
        "🦙","🦙",
    ];

    [ObservableProperty] private List<string> shuffledEmojis = [];
    [ObservableProperty] private bool animalButtonsIsVisible;
    [ObservableProperty] private bool playAgainButtonIsVisible = true;
    [ObservableProperty] private string labelText = "Time Elapsed: 0.0 seconds";
    
    [RelayCommand]
    private void PlayAgainButton()
    {
        ShuffledEmojis = [.. AnimalEmoji.OrderBy(_ => Random.Shared.Next())];
        AnimalButtonsIsVisible = true;
        PlayAgainButtonIsVisible = false; 
        Dispatcher.GetForCurrentThread()!.StartTimer(TimeSpan.FromSeconds(0.1), TimerTick);
    }

    private int tenthsOfSecondsElapsed;
    private bool TimerTick()
    {
        tenthsOfSecondsElapsed++;
        LabelText = "Time elapsed: " + (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
        if (matchesFound == 8)
        {
            tenthsOfSecondsElapsed = 0;
            return false;
        }
        return true;
    }

    private Button? lastClicked;
    private int matchesFound;

    [RelayCommand]
    private void ButtonClicked(object parameter)
    {
        if (parameter is not Button button) return;
        if (lastClicked == null)
        {
            button.BackgroundColor = Colors.Red;
            lastClicked = button;
        }
        else
        {
            if (button != lastClicked && button.Text == lastClicked.Text)
            {
                matchesFound++;
                lastClicked.Text = " ";
                button.Text = " ";
            }

            lastClicked.BackgroundColor = Colors.LightBlue;
            button.BackgroundColor = Colors.LightBlue;
            lastClicked = null;
        }

        if (matchesFound == 8)
        {
            matchesFound = 0;
            AnimalButtonsIsVisible = false;
            PlayAgainButtonIsVisible = true;
        }
    }
}
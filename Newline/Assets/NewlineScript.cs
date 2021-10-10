using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class NewlineScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable enterBtn;
    public KMSelectable leftBtn;
    public KMSelectable rightBtn;
    public TextMesh displayText;
    public TextMesh overlayText;
    public GameObject overlay;


    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;
    string[][] paragraphs = new string[10][]
    { 
        new string[] { "lorem","ipsum","dolor","sit","amet","consectetur","adipiscing","elit","nam","condimentum","lorem","quis","volutpat","lobortis","nunc","suscipit","odio","velit","sed","tempor","mauris","ullamcorper","id","interdum","et","malesuada","fames","ac","ante","ipsum","primis","in","faucibus","proin","nec","magna","ut","ligula","euismod","efficitur","in","sed","est","pellentesque","pulvinar","bibendum","viverra","pellentesque","aliquet","sapien","quis","elit","tincidunt","vitae","pretium","turpis","ullamcorper","nam","vehicula","lectus","eu","orci","tempus","ultricies","nulla","eu","sem","lacus"  },
        new string[] { "integer","eros","arcu","fermentum","vel","ipsum","eu","mattis","commodo","tortor","donec","bibendum","dapibus","risus","non","sodales","duis","ultricies","ullamcorper","eros","nunc","at","metus","diam","in","tincidunt","felis","at","nunc","accumsan","tincidunt","dapibus","ante","ultricies","suspendisse","potenti","ut","vitae","tempor","ex","sed","volutpat","magna","quis","odio","blandit","auctor","donec","scelerisque","diam","at","semper","sagittis","leo","quam","lobortis","massa","at","efficitur","augue","leo","quis","lacus" },
        new string[] { "curabitur","et","varius","libero","integer","vitae","ultricies","magna","ac","molestie","mauris","morbi","aliquet","augue","sapien","non","sodales","enim","vulputate","in","nam","sollicitudin","posuere","est","id","malesuada","nullam","convallis","nibh","metus","mauris","porttitor","mi","consectetur","nunc","mattis","quis","gravida","justo","gravida","integer","placerat","felis","eget","mattis","porttitor","urna","dolor","pharetra","tortor","at","sagittis","enim","sem","porttitor","justo","duis","eu","leo","sed","arcu","consectetur","feugiat","nam","a","gravida","ante" },
        new string[] { "quisque","eleifend","mauris","a","mi","molestie","in","accumsan","urna","volutpat","aliquam","et","imperdiet","est","at","ornare","massa","suspendisse","ipsum","massa","dictum","sed","quam","euismod","aliquam","elementum","urna","cras","quis","felis","elementum","mattis","risus","eget","euismod","leo","quisque","vel","velit","libero","proin","mi","diam","tristique","in","leo","in","sodales","finibus","lectus","donec","pulvinar","enim","ut","quam","molestie","et","dictum","ligula","efficitur","suspendisse","placerat","tortor","ut","massa","blandit","at","gravida","turpis","euismod","duis","pulvinar","dui","vel","euismod","sagittis"},
        new string[] { "pellentesque","feugiat","nisl","feugiat","luctus","risus","a","imperdiet","lectus","suspendisse","potenti","in","ornare","lacinia","tristique","vestibulum","ac","venenatis","justo","tempor","tincidunt","dui","vestibulum","vitae","justo","fermentum","mollis","eros","vitae","aliquet","ante","aliquam","sagittis","neque","et","rutrum","mollis","donec","molestie","urna","in","leo","bibendum","bibendum","cras","tristique","fringilla","lectus","et","ornare","leo","ultricies","id","quisque","vitae","lobortis","dolor","nullam","nec","lectus","vitae","sem","mattis","facilisis","sed","fermentum","leo","vel","sollicitudin","hendrerit","donec","ullamcorper","luctus","neque","sed","luctus","risus","faucibus","mattis","nulla","condimentum","mi","vel","fermentum","sodales","enim","risus","maximus","ex","non","bibendum","sapien","lacus","ac","est","aliquam","erat","volutpat","etiam","sem","nulla","dapibus","pretium","nisi","sed","auctor","cursus","sem"},
        new string[] { "donec","sodales","ipsum","vitae","augue","cursus","ut","consequat","libero","interdum","mauris","sit","amet","ipsum","porta","ullamcorper","velit","quis","hendrerit","mi","ut","vel","pharetra","turpis","quis","blandit","mauris","pellentesque","vitae","luctus","purus","sed","sagittis","mi","et","est","scelerisque","sed","malesuada","magna","molestie","maecenas","et","aliquet","magna","eget","cursus","augue","donec","rutrum","risus","non","ligula","faucibus","dictum","donec","quis","rhoncus","velit","quis","porta","lectus","integer","vitae","tortor","felis","vestibulum","suscipit","lorem","ac","luctus","feugiat","sed","vitae","vulputate","augue","proin","varius","tempus","est","vitae","hendrerit","nulla","eleifend","id","sed","id","blandit","massa","vel","feugiat","nibh","nunc","et","scelerisque","urna","fusce","scelerisque","elit","non","consectetur","rutrum","diam","massa","rhoncus","ligula","at","elementum","elit","urna","vel","arcu"},
        new string[] { "donec","non","dapibus","nibh","donec","vitae","ullamcorper","nunc","id","pharetra","nunc","sed","ut","sagittis","mauris","ut","ac","arcu","rhoncus","venenatis","ante","nec","posuere","velit","etiam","tempus","feugiat","tellus","et","tincidunt","eros","imperdiet","a","sed","eu","lacinia","mi","pellentesque","cursus","sapien","nec","facilisis","porttitor","dolor","diam","sodales","nisi","at","auctor","felis","est","at","diam","suspendisse","potenti","nam","ut","laoreet","dui","id","vehicula","ligula","nam","aliquet","a","ipsum","a","aliquet","maecenas","condimentum","dapibus","elit","sit","amet","convallis","urna","eleifend","vitae","pellentesque","interdum","ut","libero","vel","dictum","etiam","id","mauris","sapien","ut","ultrices","mauris","libero","in","egestas","sem","dapibus","nec","morbi","vel","metus","nec","leo","tincidunt","vehicula","nec","eget","odio","morbi","nulla","erat","lacinia","vitae","eros","sed","lobortis","mollis","tellus"},
        new string[] { "donec","mollis","est","eget","justo","faucibus","in","ultricies","lectus","bibendum","nunc","ornare","sagittis","erat","non","sagittis","massa","vehicula","sed","phasellus","cursus","ligula","vitae","justo","dictum","maximus","fusce","mattis","maximus","sapien","non","euismod","leo","aliquet","id","cras","sodales","vulputate","tortor","quis","placerat","risus","porttitor","vel","phasellus","in","purus","ut","risus","imperdiet","imperdiet","sed","ipsum","metus","malesuada","quis","mauris","vitae","tincidunt","auctor","neque","in","nec","gravida","felis"},
        new string[] { "duis","justo","leo","porta","ac","ante","id","ornare","gravida","metus","sed","accumsan","egestas","enim","convallis","pretium","pellentesque","ut","elementum","tortor","scelerisque","faucibus","lectus","praesent","pellentesque","euismod","mattis","mauris","ornare","condimentum","elit","vel","dictum","aenean","in","ipsum","porttitor","scelerisque","felis","sed","suscipit","sapien","suspendisse","et","enim","euismod","tempor","sem","in","bibendum","sapien","nulla","facilisi","donec","id","nibh","posuere","dictum","libero","ut","rhoncus","justo","nunc","consectetur","ut","purus","consectetur","vehicula"}, 
        new string[] { "mauris","dapibus","arcu","non","erat","dapibus","at","porttitor","lacus","pretium","quisque","posuere","aliquam","bibendum","etiam","cursus","vestibulum","lacinia","donec","tempor","et","metus","interdum","vulputate","fusce","elementum","sagittis","nisl","sit","amet","varius","orci","dignissim","quis","vestibulum","hendrerit","sit","amet","enim","vitae","venenatis","nam","magna","nunc","suscipit","non","congue","ac","porta","et","tortor"},
    };
    int startingPara;
    int numWordsFromFirst;
    int numWordsFromSecond;
    string[] firstParaArray;
    string firstParaString;
    string[] secondParaArray;
    string secondParaString;
    string fullString;
    string[] fullStringArray;
    List<int> spacePositions = new List<int>();
    List<int> spacePositionsInFull = new List<int>();
    int posInString;
    int answerPosition;
    string displayString;
    string overlayString;
    private Coroutine Blink;


    void Awake ()
    {
        moduleId = moduleIdCounter++;
        enterBtn.OnInteract += delegate () { Enter(); return false; };
        leftBtn.OnInteract += delegate () { LeftPress(); return false; };
        rightBtn.OnInteract += delegate () { RightPress(); return false; };
    }

    void Start ()
    {
        ChooseString();
        GenerateAnswer();
        DisplayInfo();
        GenerateOverlay();
        overlayText.text = overlayString;
        Blink = StartCoroutine(CursorBlink());
    }

    void ChooseString()
    {
        startingPara = UnityEngine.Random.Range(0,9);
        numWordsFromFirst = UnityEngine.Random.Range(5, 16);
        numWordsFromSecond = UnityEngine.Random.Range(5, 16);
        
        firstParaArray = paragraphs[startingPara].TakeLast(numWordsFromFirst).ToArray();
        firstParaString = firstParaArray.Join(" ");
        secondParaArray = paragraphs[startingPara + 1].Take(numWordsFromSecond).ToArray();
        secondParaString = secondParaArray.Join(" ");
        fullString = firstParaString + " " + secondParaString + " ";
        fullStringArray = fullString.Split(' ');
        for (int i = 0; i < fullString.Length; i++)
        {
            overlayString += " ";
        }
        Debug.LogFormat("[Newline #{0}] The module is taking the last {1} words of paragraph {2} and the first {3} words of paragraph {4}, resulting in a string of {5}", moduleId, numWordsFromFirst, startingPara + 1, numWordsFromSecond, startingPara + 2, fullString);
    }
    void GenerateAnswer()
    {
        answerPosition = numWordsFromFirst - 1;
        posInString = UnityEngine.Random.Range(0, fullStringArray.Length);
        Debug.LogFormat("<Newline #{0}> The cursor begins at position {1}", moduleId, posInString + 1);
        Debug.LogFormat("[Newline #{0}] You should insert a newline at the {1}th space in the string.", moduleId, answerPosition + 1);
    }
    void GenerateOverlay()
    {
        overlayString = string.Empty;
        for (int i = 0; i < displayText.text.Length; i++)
        {
            if (displayText.text[i] == ' ')
            {
                spacePositions.Add(i);
            }
        }
        for (int i = 0; i < fullString.Length; i++)
        {
            if (fullString[i] == ' ')
            {
                spacePositionsInFull.Add(i);
            }
        }
        for (int i = 0; i < displayText.text.Length; i++)
        {
            if (displayText.text[i] == '\n')
            {
                overlayString += "\n";
            }
            else if (Array.IndexOf(spacePositions.ToArray(), i) == posInString)
            {
                overlayString += "|";   
            }
            else overlayString += " ";
        }
    }
    void DisplayInfo()
    {
        displayText.text = FormatPara(fullStringArray);
        overlayText.text = overlayString;
    }

    string FormatPara(string[] input)
    {
        displayString = string.Empty;
        int ctr = 0;
        foreach (string word in input)
        {
            if (word == "\n" || word == "\n\n")
            {
                ctr = 0;
                continue;
            }
            ctr += word.Length;
            if (ctr > 31)
            {
                displayString += " \n";
                ctr = word.Length;
                displayString += word;
            }
            else
            {
                displayString += " " + word;
            }
        }
        return displayString.Substring(1,displayString.Length - 1);
    }

    void Enter()
    {
        Audio.PlaySoundAtTransform("keyPress", transform);
        enterBtn.AddInteractionPunch(1f);
        if (moduleSolved)
        {
            return;
        }
        if (posInString == answerPosition)
        {
            StopCoroutine(CursorBlink());
            overlay.SetActive(false);
            moduleSolved = true;

            displayText.text = FormatPara(firstParaArray) + "\n\n" + FormatPara(secondParaArray);

            Debug.LogFormat("[Newline #{0}] A newline was inserted at position {1}. Module solved!", moduleId, posInString + 1);
            GetComponent<KMBombModule>().HandlePass();

        }
        else
        {
            Debug.LogFormat("[Newline #{0}] A newline was inserted at position {1}. Strike!", moduleId, posInString + 1);
            GetComponent<KMBombModule>().HandleStrike();
            Audio.PlaySoundAtTransform("win7Ding", transform);
        }
    }

    void LeftPress()
    {
        if (Blink != null)
            StopCoroutine(Blink);
        Audio.PlaySoundAtTransform("keyPress", transform);
        leftBtn.AddInteractionPunch(0.1f);
        Blink = StartCoroutine(CursorBlink());
        if (moduleSolved || (posInString == 0))
            return;
        posInString--;
        GenerateOverlay();
        DisplayInfo();
    }
    void RightPress()
    {
        if (Blink != null)
            StopCoroutine(Blink);
        leftBtn.AddInteractionPunch(0.2f);
        Audio.PlaySoundAtTransform("keyPress", transform);
        Blink = StartCoroutine(CursorBlink());
        if (moduleSolved || (posInString == fullStringArray.Length - 2))
            return;
        posInString++;
        GenerateOverlay();
        DisplayInfo();
    }

    IEnumerator CursorBlink()
    {
        overlay.SetActive(true);
        while (!moduleSolved)
        {
            yield return new WaitForSecondsRealtime(0.75f);
            overlay.SetActive(false);
            yield return new WaitForSecondsRealtime(0.75f);
            overlay.SetActive(true);
        }
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use '!{0} left 10' to move the cursor left 10 times. Use '!{0} enter' to submit the answer.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand (string input)
    {
        string Command = input.Trim().ToUpperInvariant();
        List<string> parameters = Command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        if (Command.EqualsAny("ENTER", "SUBMIT", "RETURN", "RTN", "BREAK", "NEWLINE", "CHECK"))
        {
            enterBtn.OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        else if (Regex.IsMatch(Command, @"^(LEFT|RIGHT)\s+[0-9]+$"))
        {
            yield return null;
            KMSelectable whichButton = (parameters[0] == "LEFT") ? leftBtn : rightBtn;
            int count;
            if (int.TryParse(parameters[1], out count))
            {
                for (int i = 0; i < int.Parse(parameters[1]); i++)
                {
                    whichButton.OnInteract();
                    yield return "trycancel how the fuck are you doing this which one of you fuckers put that large of a number in that you need to !cancel. What.";
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else yield return "sendtochaterror Not so fast big bucko.";
        }
    }

    IEnumerator TwitchHandleForcedSolve ()
    {
        while (!moduleSolved)
        {
            if (posInString == answerPosition)
                enterBtn.OnInteract();
            if (posInString < answerPosition)
                rightBtn.OnInteract();
            if (posInString > answerPosition)
                leftBtn.OnInteract();
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}

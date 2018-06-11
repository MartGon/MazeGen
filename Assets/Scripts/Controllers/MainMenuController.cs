using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Windows
    public GameObject Inteface;
    public GameObject MainMenu;
    public GameObject Options;
    public GameObject Score;

    // Misc values
    public Text rows;
    public Text cols;

    // Sliders
    public Slider rowSlider;
    public Slider colSlider;

    // DropDowns
    public Dropdown algDropdown;

    // Toggle
    public Toggle fogToggle;

    // Input
    public InputField seedInput;

    // Dictionary
    private Dictionary<string, MazeGenerator.GenAlgorithm> algs;

    // Score
    public Text generatedMazes;
    public Text completedMazes;

    public Text lastResolutionTime;
    public Text maxResolutionTime;
    public Text minResolutionTime;

    void Start()
    {
        // Activamos menú, desactivamos opciones
        MainMenu.SetActive(true);
        Options.SetActive(false);

        // Inicializado en caso de ser la primera vez
        if(PlayerPrefs.GetInt("rows") == 0)
            PlayerPrefs.SetInt("rows", 5);
        if (PlayerPrefs.GetInt("cols") == 0)
            PlayerPrefs.SetInt("cols", 5);

        // Rellenamos el diccionario
        algs = new Dictionary<string, MazeGenerator.GenAlgorithm>();
        algs.Add("Depth First Search", MazeGenerator.GenAlgorithm.MAZE_ALGORITHM_DEPTH_FIRST_SEARCH);
        algs.Add("Prim's Algorithm", MazeGenerator.GenAlgorithm.MAZE_ALGORITHM_PRIMS);
        algs.Add("Recursive Division", MazeGenerator.GenAlgorithm.MAZE_ALGORITHM_RECURSIVE_DIVISION);

        // Rellenamos el dropdown
        List < string >  options = new List<string>();
        foreach(KeyValuePair<string, MazeGenerator.GenAlgorithm> entry in algs)
            options.Add(entry.Key);

        algDropdown.ClearOptions();
        algDropdown.AddOptions(options);

        // Rellenamos las Opciones
        rows.text = PlayerPrefs.GetInt("rows").ToString();
        cols.text = PlayerPrefs.GetInt("cols").ToString();
        rowSlider.value = PlayerPrefs.GetInt("rows");
        colSlider.value = PlayerPrefs.GetInt("cols");
        algDropdown.value = PlayerPrefs.GetInt("alg");

        // Rellenamos la puntuación
        generatedMazes.text = PlayerPrefs.GetInt("totalMazes").ToString();
        completedMazes.text = PlayerPrefs.GetInt("completedMazes").ToString();
        //preferredAlgorithm.text = PlayerPrefs.

        lastResolutionTime.text = PlayerPrefs.GetFloat("lastTime").ToString() + " s";
        maxResolutionTime.text = PlayerPrefs.GetFloat("maxTime").ToString() + " s";
        minResolutionTime.text = PlayerPrefs.GetFloat("minTime").ToString() + " s";

    }

    // Main menu

    public void playGameButton()
    {
        SceneManager.LoadScene("MazeGame");

        if (seedInput.text.Length != 0)
            PlayerPrefs.SetInt("seed", int.Parse(seedInput.text));
        else
            PlayerPrefs.SetInt("seed", 0);

        PlayerPrefs.SetInt("totalMazes", PlayerPrefs.GetInt("totalMazes") + 1);
    }

    public void scoreboardButton()
    {
        Score.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void OptionsButton()
    {
        Options.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    // Options Menu

    public void OptionsAcceptButton()
    {
        PlayerPrefs.SetInt("rows", int.Parse(rows.text));
        PlayerPrefs.SetInt("cols", int.Parse(cols.text));
        PlayerPrefs.SetInt("alg", (int)algs[algDropdown.captionText.text]);
        PlayerPrefs.SetInt("fog", fogToggle.isOn ? 1 : 0);

        Options.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void handleRowSlider()
    {
        rows.text = rowSlider.value.ToString();
    }

    public void handleColSlider()
    {
        cols.text = colSlider.value.ToString();
    }

    // Score Menu
    public void ScoreAcceptButton()
    {
        Score.SetActive(false);
        MainMenu.SetActive(true);
    }

}

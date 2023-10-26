using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIConverterController : MonoBehaviour
{
    [Header("Converter Window")]
    [SerializeField] private GameObject _convertationWindow;
    [SerializeField] private GameObject _warningWindow;
    [SerializeField] private GameObject _convertationButton;

    [Header("Tool Bar")]
    [SerializeField] private Text _toolBarResourceA;
    [SerializeField] private Text _toolBarResourceB;

    [Header("Miscs")]
    [SerializeField] private InputField _inputResourceA;
    [SerializeField] private Text _outputResourceB;
    [SerializeField] private Slider _convertSldier;

    [Header("Game Resources")]
    [SerializeField] private GameResourcesController _gameResourcesController;

    // Intermediate indicators of resources
    private int _interimResourceA = 1;
    private int _interimResourceB = 2;

    private void Start() => ResetConverter();

    // Update indicators on the canvas
    public void TextFieldDrawcall()
    {
        if (int.TryParse(_inputResourceA.text, out _interimResourceA))
            _interimResourceA = Int32.Parse(_inputResourceA.text);

        if (_interimResourceA > _gameResourcesController.ResourceA)
            _inputResourceA.text = _gameResourcesController.ResourceA.ToString();

        _interimResourceB = _interimResourceA * 2;
        _outputResourceB.text = _interimResourceB.ToString();

        _convertSldier.value = _interimResourceA;
    }

    // Update indicators on the canvas
    public void SliderDrawcall()
    {
        if ((int)_convertSldier.value == 0)
            _interimResourceA = 1;
        else
            _interimResourceA = (int)_convertSldier.value;

        _interimResourceB = _interimResourceA * 2;
        _outputResourceB.text = _interimResourceB.ToString();
        _inputResourceA.text = _interimResourceA.ToString();
    }

    // Reset converter menu
    public void ResetConverter()
    {
        _inputResourceA.text = "1";
        _convertSldier.minValue = 0;
        _convertSldier.maxValue = _gameResourcesController.ResourceA;
        _convertSldier.value = 0;
    }

    // Add resource to convertation
    public void AddResource() 
    {
        if (_interimResourceA + 10 < _gameResourcesController.ResourceA)
            _interimResourceA += 10;
        else
            _interimResourceA = _gameResourcesController.ResourceA;

        _inputResourceA.text = _interimResourceA.ToString();
    }

    // Subtract resource to convertation
    public void SubtractResource()
    {
        if (_interimResourceA - 10 > 1)
            _interimResourceA -= 10;
        else
            _interimResourceA = 1;

        _inputResourceA.text = _interimResourceA.ToString();
    }

    // Apply convertation and close the window
    public void ApplyConvertation()
    {
        _interimResourceA = Int32.Parse(_inputResourceA.text);
        _gameResourcesController.ConvertAToB(_interimResourceA);

        _toolBarResourceA.text = _gameResourcesController.ResourceA.ToString() + " A";
        _toolBarResourceB.text = _gameResourcesController.ResourceB.ToString() + " B";

        CloseConvertationWindow();
    }

    // Decline convertation and close the window
    public void CloseConvertationWindow()
    {
        _convertationWindow.SetActive(false);
        _convertationButton.SetActive(true);
        ResetConverter();
    }

    // Open converter window
    public void OpenConvertationWindow()
    {
        if (_gameResourcesController.ResourceA == 0)
            StartCoroutine(ShowWarningMassage());
        else
        {
            StopAllCoroutines();
            _convertationWindow.SetActive(true);
            _convertationButton.SetActive(false);
        }
    }

    // COROUTINE shows warning for 2 seconds
    private IEnumerator ShowWarningMassage()
    {
        _warningWindow.SetActive(true);
        _convertationButton.SetActive(false);
        yield return new WaitForSeconds(2f);
        _warningWindow.SetActive(false);
        _convertationButton.SetActive(true);
        yield break;
    }
}
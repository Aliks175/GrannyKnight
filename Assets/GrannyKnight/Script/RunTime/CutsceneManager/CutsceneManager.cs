using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    // ������ Singleton ��� ���� ����� ����� ��������� � CutsceneManager ����� CutsceneManager.Instance.�����������������������������()
    public static CutsceneManager Instance;

    // ���� �� �������� �������, � ������� ���� Key � Value ������� � ���������� ����� ���������� � Dictionary "cutsceneDataBase"
    // ������ �� ��� ������ ��� ���� ��������� Dictionary �� ������������ � ����������
    [SerializeField] private List<CutsceneStruct> cutscenes = new List<CutsceneStruct>();

    // ���� ������ ���������� ��� �������� ������� �� ����� ���������, � � ���������� ����� ���������� ���� �������� �� ������
    // ��� ��� ��� Dictionary ��������� � ��������� �� ����� ��������� � ���� �� ������ ������� ��� ��� - CutsceneManager.cutsceneDataBase["���� ������ ��������"]
    public static Dictionary<string, GameObject> cutsceneDataBase = new Dictionary<string, GameObject>();

    // ������ � ���� �������� ������� ������������� � ������� ������, ���� �� ����� �������� ������ �� ������������� - ��� ����� null
    public static GameObject activeCutscene;

    private void Awake()
    {
        // ������ Singleton
        Instance = this;

        // �������� ����� ������������� ���� ������ � ����������
        InitializeCutsceneDataBase();

        // ���������� �� ���� ��������� � ��������� �� (����� ��� ������� ���� �� ����������� ��������)
        foreach (var cutscene in cutsceneDataBase)
        {
            if (cutscene.Value != null)  cutscene.Value.SetActive(false);
        }
    }

    // ����� � ������� �� ��������� Dictionary cutsceneDataBase
    private void InitializeCutsceneDataBase()
    {
        // ����� ����������� �� ������ ������ ������� ���� ���� ������
        cutsceneDataBase.Clear();

        // ��������� cutsceneDataBase ������� � ���������� ������� �� ������ � ����� cutscenes
        for (int i = 0; i < cutscenes.Count; i++)
        {
            cutsceneDataBase.Add(cutscenes[i].cutsceneKey, cutscenes[i].cutsceneObject);
        }
    }

    // ����� ��� ��������� �������� �� �����
    public void StartCutscene(string cutsceneKey)
    {
        // ���� cutsceneDataBase �� ������� �������� � cutsceneKey �� ��������� �� ���� � ������� � �� ��������� ���� ��������� �����
        if (!cutsceneDataBase.ContainsKey(cutsceneKey))
        {
            Debug.LogError($"�������� c ������ \"{cutsceneKey}\" ���� � cutsceneDataBase");
            return;
        }

        // ���� ������ ������������� �������� � �� �������� ������� � ���� ������ Ũ �� �� ������ �������� ���������� ������
        if (activeCutscene != null)
        {
            if (activeCutscene == cutsceneDataBase[cutsceneKey])
            {
                return;
            }
        }

        // ����������� �������� ��������
        activeCutscene = cutsceneDataBase[cutsceneKey];

        // ��������� ��� ��������
        foreach (var cutscene in cutsceneDataBase)
        {
            if (cutscene.Value != null)  cutscene.Value.SetActive(false);
        }

        // �������� �� �������� ������� ����� �������
        cutsceneDataBase[cutsceneKey].SetActive(true);
    }

    // ����� ������� ��������� ������� ��������
    public void EndCutscene()
    {
        if (activeCutscene != null)
        {
            activeCutscene.SetActive(false);
            activeCutscene = null;
        }
    }
}

// ��������� ������� ��� �����, ����� ����� ����������� ��� �������� � Key � Value � Dictionary cutsceneDataBase
[System.Serializable]
public struct CutsceneStruct
{
    public string cutsceneKey;
    public GameObject cutsceneObject;
}

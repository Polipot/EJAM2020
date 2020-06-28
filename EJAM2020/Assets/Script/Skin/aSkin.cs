using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class aSkin : MonoBehaviour
{
    IAManager IAM;
    IAMovement myIA;
    Player_Movement myPlayer;
    bool AlreadyAsignedOnTarget;

    [Header("Parties du Skin")]
    public SpriteRenderer Corps;
    public SpriteRenderer Tête, EpauleDroite, BrasDroit, MainDroite, EpauleGauche, BrasGauche, MainGauche;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSkin(IAMovement theIA)
    {
        SpriteRenderer[] LesSprites = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < LesSprites.Length; i++)
        {
            switch (LesSprites[i].gameObject.name)
            {
                case "Corps":
                    Corps = LesSprites[i];
                    break;
                case "Tête":
                    Tête = LesSprites[i];
                    break;
                case "EpauleDroite":
                    EpauleDroite = LesSprites[i];
                    break;
                case "BrasDroite":
                    BrasDroit = LesSprites[i];
                    break;
                case "MainDroite":
                    MainDroite = LesSprites[i];
                    break;
                case "EpauleGauche":
                    EpauleGauche = LesSprites[i];
                    break;
                case "BrasGauche":
                    BrasGauche = LesSprites[i];
                    break;
                case "MainGauche":
                    MainGauche = LesSprites[i];
                    break;
            }
        }

        myIA = GetComponentInParent<IAMovement>();
        IAM = IAManager.Instance;

        if (theIA.myType == Type.Guard)
        {
            Corps.sprite = Resources.Load<Sprite>("Skins/Guard/Corps");
            Tête.sprite = Resources.Load<Sprite>("Skins/Guard/Tête");
            EpauleDroite.sprite = Resources.Load<Sprite>("Skins/Guard/Epaule");
            EpauleGauche.sprite = Resources.Load<Sprite>("Skins/Guard/Epaule");
            BrasDroit.sprite = Resources.Load<Sprite>("Skins/Guard/Bras");
            BrasGauche.sprite = Resources.Load<Sprite>("Skins/Guard/Bras");
            MainDroite.sprite = Resources.Load<Sprite>("Skins/Guard/Main");
            MainGauche.sprite = Resources.Load<Sprite>("Skins/Guard/Main");

            theIA.SkinChemin = "Skins/Guard";
        }

        else if(theIA.myType == Type.Civilian && AlreadyAsignedOnTarget == false)
        {
            DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Skins/Civil");
            DirectoryInfo[] info = dir.GetDirectories("*.*");
            int count = dir.GetDirectories().Length;

            int rnd = Random.Range(1, count + 1);

            Corps.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Corps");
            Tête.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Tête");
            EpauleDroite.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Epaule");
            EpauleGauche.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Epaule");
            BrasDroit.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Bras");
            BrasGauche.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Bras");
            MainDroite.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Main");
            MainGauche.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Main");

            theIA.SkinChemin = "Skins/Civil/Civil_" + rnd;

            if (myIA.IsTarget)
            {
                IAM.AddPortraits(theIA.SkinChemin);
                AlreadyAsignedOnTarget = true;
            }
        }

        else if (theIA.myType == Type.Policeman)
        {
            Corps.sprite = Resources.Load<Sprite>("Skins/Policeman/Corps");
            Tête.sprite = Resources.Load<Sprite>("Skins/Policeman/Tête");
            EpauleDroite.sprite = Resources.Load<Sprite>("Skins/Policeman/Epaule");
            EpauleGauche.sprite = Resources.Load<Sprite>("Skins/Policeman/Epaule");
            BrasDroit.sprite = Resources.Load<Sprite>("Skins/Policeman/Bras");
            BrasGauche.sprite = Resources.Load<Sprite>("Skins/Policeman/Bras");
            MainDroite.sprite = Resources.Load<Sprite>("Skins/Policeman/Main");
            MainGauche.sprite = Resources.Load<Sprite>("Skins/Policeman/Main");

            theIA.SkinChemin = "Skins/Policeman";
        }
    }

    public void LoadSkin(Player_Movement thePlayer, string path = "Skins/Civil/Civil_1")
    {
        SpriteRenderer[] LesSprites = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < LesSprites.Length; i++)
        {
            switch (LesSprites[i].gameObject.name)
            {
                case "Corps":
                    Corps = LesSprites[i];
                    break;
                case "Tête":
                    Tête = LesSprites[i];
                    break;
                case "EpauleDroite":
                    EpauleDroite = LesSprites[i];
                    break;
                case "BrasDroite":
                    BrasDroit = LesSprites[i];
                    break;
                case "MainDroite":
                    MainDroite = LesSprites[i];
                    break;
                case "EpauleGauche":
                    EpauleGauche = LesSprites[i];
                    break;
                case "BrasGauche":
                    BrasGauche = LesSprites[i];
                    break;
                case "MainGauche":
                    MainGauche = LesSprites[i];
                    break;
            }
        }

        Corps.sprite = Resources.Load<Sprite>(path + "/Corps");
        Tête.sprite = Resources.Load<Sprite>(path + "/Tête");
        EpauleDroite.sprite = Resources.Load<Sprite>(path + "/Epaule");
        EpauleGauche.sprite = Resources.Load<Sprite>(path + "/Epaule");
        BrasDroit.sprite = Resources.Load<Sprite>(path + "/Bras");
        BrasGauche.sprite = Resources.Load<Sprite>(path + "/Bras");
        MainDroite.sprite = Resources.Load<Sprite>(path + "/Main");
        MainGauche.sprite = Resources.Load<Sprite>(path + "/Main");

        thePlayer.SkinChemin = path;
    }
}

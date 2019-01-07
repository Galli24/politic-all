using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

struct GameQuestion
{
    public int ansNbr;
    public string question;
    public Dictionary<string, Vector3> answers;

    public GameQuestion(int _answers, string _question, string _answer1, string _answer2, string _answer3, string _answer4,
        Vector3 _a1vals, Vector3 _a2vals, Vector3 _a3vals, Vector3 _a4vals)
    {
        ansNbr = _answers;
        question = _question;
        answers = new Dictionary<string, Vector3>();
        answers.Add(_answer1, _a1vals);
        answers.Add(_answer2, _a2vals);
        if (_answer3 != "")
            answers.Add(_answer3, _a3vals);
        if (_answer4 != "")
            answers.Add(_answer4, _a4vals);
    }
}

struct PersQuestion
{
    public int answers;
    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public Vector3 a1vals;
    public Vector3 a2vals;
    public Vector3 a3vals;
    public Vector3 a4vals;
    public Vector3 a1caracs;
    public Vector3 a2caracs;
    public Vector3 a3caracs;
    public Vector3 a4caracs;

    public PersQuestion(int _answers, string _question, string _answer1, string _answer2, string _answer3, string _answer4, 
        Vector3 _a1vals, Vector3 _a2vals, Vector3 _a3vals, Vector3 _a4vals,
        Vector3 _a1caracs, Vector3 _a2caracs, Vector3 _a3caracs, Vector3 _a4caracs)
    {
        answers = _answers;
        question = _question;
        answer1 = _answer1;
        answer2 = _answer2;
        answer3 = _answer3;
        answer4 = _answer4;
        a1vals = _a1vals;
        a2vals = _a2vals;
        a3vals = _a3vals;
        a4vals = _a4vals;
        a1caracs = _a1caracs;
        a2caracs = _a2caracs;
        a3caracs = _a3caracs;
        a4caracs = _a4caracs;
    }
}

public class Questions : MonoBehaviour {

    public static Questions instance;

    List<GameQuestion> gameQuestions = new List<GameQuestion>();
    List<PersQuestion> persQuestions = new List<PersQuestion>();

    [SerializeField]
    int farmersInfluence = 50;
    [SerializeField]
    int militaryInfluence = 50;
    [SerializeField]
    int banksInfluence = 50;
    [SerializeField]
    int globalInfluence = 50;

    [SerializeField]
    int charisma = 5;
    [SerializeField]
    int niceBoiiii = 5;
    [SerializeField]
    int generosity = 5;
    [SerializeField]
    int ratio = 7;

    bool next_question = false;

    float bankmult = 1;
    float militarymult = 1;
    float peoplemult = 1;

    bool setup = false;
    bool game = false;
    bool gameStarted = false;

    [SerializeField]
    string goodpoint = "-";
    [SerializeField]
    string badpoint = "-";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (!setup)
            UIObjs.instance.continue_button.SetActive(false);
        if (!setup)
        {
            stockPersQuestions();
            stockGameQuestions();
            setup = true;
        }
        if (!game)
            doPersQuestions(0);
    }

    void Update()
    {
        if (!game)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                game = true;
                UIObjs.instance.armyFill.GetComponent<Image>().fillAmount = 0.5f;
                UIObjs.instance.bankFill.GetComponent<Image>().fillAmount = 0.5f;
                UIObjs.instance.peopleFill.GetComponent<Image>().fillAmount = 0.5f;
            }
        }

        if (game && !gameStarted)
        {
            gameStarted = true;
            UIObjs.instance.goodPoint.GetComponent<Text>().text = "Point fort: " + goodpoint;
            UIObjs.instance.badPoint.GetComponent<Text>().text = "Point faible: " + badpoint;
            if (goodpoint.Equals("Kaarismatique"))
            {
                UIObjs.instance.ArmyMe.SetActive(true);
                UIObjs.instance.PeopleMe.SetActive(false);
            }
            else if (goodpoint.Equals("Généreux"))
            {
                UIObjs.instance.BankMe.SetActive(true);
                UIObjs.instance.PeopleMe.SetActive(false);
            }
            doGameQuestions();
        }
    }

    void    make_mult()
    {
        if (charisma > niceBoiiii && charisma > generosity)
            militarymult = 1.5f;
        if (niceBoiiii > charisma && niceBoiiii > generosity)
            peoplemult = 1.5f;
        if (generosity > charisma && generosity > niceBoiiii)
            bankmult = 1.5f;
        if (generosity < charisma && generosity < niceBoiiii)
            bankmult = 0.75f;
        if (charisma < generosity && charisma < niceBoiiii)
            militarymult = 0.75f;
        if (niceBoiiii < generosity && niceBoiiii < charisma)
            peoplemult = 0.75f;
    }

    void    stockPersQuestions()
    {
        persQuestions.Add(new PersQuestion(2, "Que voulez vous faire ? Aidez le peuple et favoriser le coté social de votre campagne ou favoriser les entreprises et ainsi développer l'économie du pays", "Peuple", "Economie", "", "", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(1, -1, 0), new Vector3(-1, 1, 0), Vector3.zero, Vector3.zero));
        persQuestions.Add(new PersQuestion(2, "Accepter les migrants ou fermer les frontières ?", "Migrants", "Fermeture des frontières", "", "", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(0, 1, 1), new Vector3(1, 1, 0), Vector3.zero, Vector3.zero));
        persQuestions.Add(new PersQuestion(2, "Pain au Chocolat ou Chocolatine ?", "Chocolatine", "Chocolatine", "", "", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero));
        persQuestions.Add(new PersQuestion(2, "Si vous êtes mis en examen à raison que feriez-vous ?", "Démentir et garder la face", "S'excuser et endorser vos responsaabilités", "", "", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(1, - 1, 0), new Vector3(-1, 1, 0), Vector3.zero, Vector3.zero));
        persQuestions.Add(new PersQuestion(4, "Pour ou contre les tortues dans la production d'engrais artisanal ?", "Oui", "Pourquoi pas", "Bien sûr que non ! Protégeons l'environnement !!", "POUR !", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 0, 0), new Vector3(1, 1, 1)));
        persQuestions.Add(new PersQuestion(4, "Pour ou contre la consommation de drogue en écoutant Salut C'est Cool ?", "Bien sûr !", "Heuuu...", "SUREMENT PAS !", "Trance & Acid", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(2, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(2, 0, 0)));
        persQuestions.Add(new PersQuestion(3, "Pas trop déçu de la mort de Jon Snow ?", "Je connais pas...", "Georges R R. Martin est un salaud", "Ca va imotep", "", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(-1, -1, -1), new Vector3(0, 1, 0), new Vector3(1, -1, -1), Vector3.zero));
        persQuestions.Add(new PersQuestion(3, "Que pensez vous des Anges de la Téléréalité ?", "Sauvez l'humanité", "Je huuuuuurle", "J'adore ce genre d'emmisions politiques", "", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(1, 0, 0), new Vector3(-1, -1, -1), new Vector3(-1, -1, -1), Vector3.zero));
        persQuestions.Add(new PersQuestion(2, "Un futur tour de France à pied pour rencontrer le peuple ?", "Bien sûr ! Allons boire un verre !", "Surtout pas ! N'approchons pas le petit milieu parisien...", "", "", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(0, 1, 1), new Vector3(1, 0, 0), Vector3.zero, Vector3.zero));
        persQuestions.Add(new PersQuestion(2, "Dumbledore VS Severus ?", "Dumbledore", "Severus", "", "", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(1, 0, -1), new Vector3(-1, 1, 1), Vector3.zero, Vector3.zero));
        persQuestions.Add(new PersQuestion(2, "Ne plus être vu mais protéger ses proches VS Être connu mais voir sa famille mourir ?", "Famille", "Popularité", "", "", Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(-2, 1, 0), new Vector3(1, -2, 0), Vector3.zero, Vector3.zero));
        persQuestions.Add(new PersQuestion(2, "Où suis-je ?! ", "à Lyon ?", " à Paaaaaris", "", "", new Vector3(-1, 0, 1), new Vector3(1, 0, -1), Vector3.zero, Vector3.zero, new Vector3(-2, 1, 0), new Vector3(1, -2, 0), Vector3.zero, Vector3.zero));
    }

    void doPersQuestions(int i)
    {
        PersQuestion tmp;

        next_question = false;
        tmp = persQuestions[i];

        if (i == 11)
            AudioManager.instance.PlaySound2D("OU SUIS JE");

        UIObjs.instance.question.GetComponent<Text>().text = tmp.question;

        UIObjs.instance.answer1_obj.SetActive(true);
        UIObjs.instance.answer2_obj.SetActive(true);
        UIObjs.instance.answer3_obj.SetActive(false);
        UIObjs.instance.answer4_obj.SetActive(false);

        if (tmp.answers >= 3)
            UIObjs.instance.answer3_obj.SetActive(true);
        if (tmp.answers == 4)
            UIObjs.instance.answer4_obj.SetActive(true);

        UIObjs.instance.answer1.GetComponent<Text>().text = tmp.answer1;
        UIObjs.instance.answer2.GetComponent<Text>().text = tmp.answer2;
        UIObjs.instance.answer3.GetComponent<Text>().text = tmp.answer3;
        UIObjs.instance.answer4.GetComponent<Text>().text = tmp.answer4;

        UIObjs.instance.answer1_obj.GetComponent<ButtonData>().setInfluenceData(tmp.a1vals);
        UIObjs.instance.answer2_obj.GetComponent<ButtonData>().setInfluenceData(tmp.a2vals);
        UIObjs.instance.answer3_obj.GetComponent<ButtonData>().setInfluenceData(tmp.a3vals);
        UIObjs.instance.answer4_obj.GetComponent<ButtonData>().setInfluenceData(tmp.a4vals);

        UIObjs.instance.answer1_obj.GetComponent<ButtonData>().setCaracsData(tmp.a1caracs);
        UIObjs.instance.answer2_obj.GetComponent<ButtonData>().setCaracsData(tmp.a2caracs);
        UIObjs.instance.answer3_obj.GetComponent<ButtonData>().setCaracsData(tmp.a3caracs);
        UIObjs.instance.answer4_obj.GetComponent<ButtonData>().setCaracsData(tmp.a4caracs);

        StartCoroutine(wait_for_response(++i, persQuestions.Count));
    }

    IEnumerator wait_for_response(int i, int max)
    {
        while (!next_question)
            yield return null;
        if (i < max)
            doPersQuestions(i);
        else
        {
            UIObjs.instance.question.GetComponent<Text>().text = "";
            UIObjs.instance.answer1_obj.SetActive(false);
            UIObjs.instance.answer2_obj.SetActive(false);
            UIObjs.instance.answer3_obj.SetActive(false);
            UIObjs.instance.answer4_obj.SetActive(false);
            make_mult();
            who_u_are();
        }
    }

    void who_u_are()
    {
        UIObjs.instance.continue_button.SetActive(true);

        if (militarymult == 1 && peoplemult == 1 && bankmult == 1)
            UIObjs.instance.question.GetComponent<Text>().text = "Vous êtes quelqu'un de neutre, vous saurez plaire à tout votre peuple, mais vous restez quelqu'un de très médiocre, vous n'irez sûrement pas bien loin.";
        else if (militarymult > 1)
        {
            goodpoint = "Kaarismatique";
            if (peoplemult == 1 && bankmult < 1)
                UIObjs.instance.question.GetComponent<Text>().text = "Vous êtes un grand locuteur, vous ne manquez pas de Kaarisme mais votre générosité n'est pas au rendez-vous, criez ne vous amènera pas l'amour du peuple, heureusement que vos militaires sont là...";
            else if (peoplemult < 1 && bankmult == 1)
                UIObjs.instance.question.GetComponent<Text>().text = "Parlez tant que vous le pouvez, mais le peuple ne vous aime vraiment pas, vous avez du boulot pour que votre éléction ne soit pas la pire de l'humanité.";
            else
                UIObjs.instance.question.GetComponent<Text>().text = "Vous arrivez à donner des ordres et vous faire comprendre, encore faut-il que cela suffise, la parole est d'argent, le silence est d'or, vos ennemis pourraient préférer l'or.";
        }
        else if (peoplemult > 1)
        {
            goodpoint = "Gentil";
            if (militarymult < 1 && bankmult == 1)
                UIObjs.instance.question.GetComponent<Text>().text = "Le peuple vous adore, gloire à vous, le monde vous souri, mais n'oubliez pas, César n'est pas mort de la main du peuple, implantez vous des yeux dans le dos.";
            else if (militarymult == 1 && bankmult < 1)
                UIObjs.instance.question.GetComponent<Text>().text = "Qu'est-ce qui est le plus intéressant, l'amour ou l'argent ? Vous ne partagez qu'un des deux, il serait peut-être temps d'arrêter de cacher votre fortune dans la tombe de mamie.";
            else
                UIObjs.instance.question.GetComponent<Text>().text = "Seul le peuple vous aime, vous ne viendriez pas du sud vous ?";
        }
        else if (bankmult > 1)
        {
            goodpoint = "Généreux";
            if (militarymult < 1 && peoplemult == 1)
                UIObjs.instance.question.GetComponent<Text>().text = "Vous ne gardez rien pour vous, mais qui croira les paroles d'une personne qui n'a aucune autorité ?";
            else if (militarymult == 1 && peoplemult < 1)
                UIObjs.instance.question.GetComponent<Text>().text = "Rendez l'argent.";
            else
                UIObjs.instance.question.GetComponent<Text>().text = "Money, money, money, vous comptez seulement donnez vos biens, il faudrait peut-être réfléchir aux conséquences, l'argent ne tombe pas des arbres (vous par contre...).";
        }
        else if (bankmult < 1 && militarymult == 1 && peoplemult == 1)
            UIObjs.instance.question.GetComponent<Text>().text = "Les banques vous haïssent, et personne d'autre ne vous soutient, le chemin va être long...";
        else if (militarymult < 1 && bankmult == 1 && peoplemult == 1)
            UIObjs.instance.question.GetComponent<Text>().text = "Vous avez votre armée à dos, cherchez vous d'autres alliés, pour le moment vous êtes juste mal parti...";
        else if (peoplemult < 1 && bankmult == 1 && militarymult == 1)
            UIObjs.instance.question.GetComponent<Text>().text = "Le peuple ne vous aime pas, mais, comment êtes vous arrivé ici au final ??";

        if (bankmult < 1)
            badpoint = "Avare";
        else if (peoplemult < 1)
            badpoint = "Cruel";
        else if (militarymult < 1)
            badpoint = "JuL";
    }

    void stockGameQuestions()
    {
        gameQuestions.Add(new GameQuestion(2, "Votre famille attrape la grippe aviaire. Vous avez un meeting normalement prévu mais vous devez leur rendre visite.", "Vous annulez votre meeting et rejoignez votre tendre famille", "Ballec frère, le temps c'est de l'argent", "", "", new Vector3(1 * ratio, -1 * ratio, -1 * ratio), new Vector3(-1 * ratio, 1 * ratio, 1 * ratio), Vector3.zero, Vector3.zero));
        gameQuestions.Add(new GameQuestion(2, "Une manifestation est sur le point de se transformer en émeute devant votre QG", "Vous sortez pour discuter, calmez les foules, le tout entouré de vos gardes du corps", "Je n'ai rien à dire à des barbares !!", "", "", Vector3.zero, new Vector3(-1 * ratio, 0, 0), Vector3.zero, Vector3.zero));
        gameQuestions.Add(new GameQuestion(2, "Lors d'une interview, un spécialiste vous pose une question concernant la pathologie des équipements de génie climatiques et vous demande votre avis sur l'amélioration de la qualité et à la diminution de la sinistralité", "Mais oui c'est clair", "42", "", "", new Vector3(1 * ratio, 0, 0), new Vector3(-1 * ratio, 0, 0), Vector3.zero, Vector3.zero));
        gameQuestions.Add(new GameQuestion(3, "Un attentat éclate, le pays est en sang. Vous devez réagir et faire passer un message à votre pays adoré, quel sera-t-il ?", "YOLO", "Restez fort et uni", "M'en bat les couilles frère", "", new Vector3(2 * ratio, 2 * ratio, 2 * ratio), new Vector3(-2 * ratio, -2 * ratio, -2 * ratio), new Vector3(2 * ratio, 2 * ratio, 2 * ratio), Vector3.zero));
        gameQuestions.Add(new GameQuestion(2, "Vous êtes invité à une soirée regroupant les personnalités influentes du moment. Que faites-vous ?", "J'accepte, cela peut peut-être m'aider pour mon avenir", "Je refuse poliment, je souhaite faire mon parcours seul", "", "", new Vector3(-1 * ratio, 0 * ratio, 1 * ratio), new Vector3(1 * ratio, 0 * ratio, -1 * ratio), Vector3.zero, Vector3.zero));
        gameQuestions.Add(new GameQuestion(3, "Vous apprenez après des années que n'avoir qu'un testicule n'est pas normal", "*TRIGGERED*", "Je m'en bat la couille", "Je n'ai qu'un ovaire", "", Vector3.zero, new Vector3(1 * ratio, 0, 0), new Vector3(1 * ratio, 0, 0 * ratio), Vector3.zero));
        gameQuestions.Add(new GameQuestion(4, "Ceci est la question piège :", "La réponse A", "La réponse B", "La réponse C", "La réponse D", new Vector3(-1 * ratio, 0, 0), new Vector3(0, 0, -1 * ratio), new Vector3(0, -1 * ratio, 0), new Vector3(1 * ratio, 1 * ratio, 1 * ratio)));
        gameQuestions.Add(new GameQuestion(2, "Une des entreprises dans laquelle vous avec investi de l'argent est en train de faire faillite", "Je vend mes parts et amortis financièrement la perte", "Je réinjecte de l'argent pour aider cette entreprise à remonter la pente", "", "", new Vector3(0, 0, 1 * ratio), new Vector3(0, 0, -1 * ratio), Vector3.zero, Vector3.zero));
        gameQuestions.Add(new GameQuestion(3, "Vous avez faim. Vous cherchez un endroit où subvenir à vos besoins.", "Un fast-food, pour paraître bien chez les prolos", "Un restau mondain pour le caviar", "A la maison", "", new Vector3(1 * ratio, 0, 0), new Vector3(0, 0, 1 * ratio), new Vector3(0, 0, 0), Vector3.zero));
        gameQuestions.Add(new GameQuestion(2, "Vous piochez la carte prison, que faire ?", "Obéir. Après tout il faut respecter les règles !", "Vous faites jouer votre immunité parlementaire", "", "", new Vector3(1 * ratio, 0, 0), new Vector3(-1 * ratio, -1 * ratio, 0), Vector3.zero, Vector3.zero));
        gameQuestions.Add(new GameQuestion(2, "Votre attaché de presse vous demande d'aller dans la baignoire de Jeremstar pour une interview, vous assurant que ça vous rendra plus crédible envers la jeunesse, que faites-vous ?", "Vous HUUUUUUURLEZ et revettez votre plus beau moullebite", "Vous refusez et brulez votre attaché de presse en place publique", "", "", new Vector3(1 * ratio, 0, 0), new Vector3(-1 * ratio, -1 * ratio, 0), Vector3.zero, Vector3.zero));
        gameQuestions.Add(new GameQuestion(2, "Les travailleurs s'énervent et accusent le gouvernement d'oublier le peuple et de favoriser le profit des grandes entreprises. Comment réagissez-vous ?", "Les ignorer, seul le profit global du pays importe.", "Se retourner contre les entreprises et soutenir le peuple.", "", "", new Vector3(-1 * ratio, 0, 1 * ratio), new Vector3(1 * ratio, 0, -1 * ratio), Vector3.zero, Vector3.zero));
        gameQuestions.Add(new GameQuestion(3, "Salamèche, Carapuce ou Bulbizarre ?", "Salamèche", "Carapuce", "Bulbizarre", "", new Vector3(0, 1, 0), new Vector3(1 * ratio, 0, 0), new Vector3(0, 0, 0), Vector3.zero));
        gameQuestions.Add(new GameQuestion(2, "\"BusinessIsBusiness\", une entreprise chinoise vous propose un placement de produit lors d'un de vos meeting, acceptez-vous ?", "Oui", "Non", "", "", new Vector3(-1 * ratio, 1 * ratio, 0), new Vector3(1 * ratio, -1 * ratio, 0), Vector3.zero, Vector3.zero));
        gameQuestions.Add(new GameQuestion(3, "Vous êtes soupçonné d'avoir mis en palce le plus gros détournement de fond de l'histoire de la nation. Vous êtes mis en examen mais vous n'êtes heureusement pas coupable. Défendez-vous !", "J'en assusme l'entière responsabilité", "Appelez les hendecks", "Wallah c'est pas moi", "", new Vector3(-1 * ratio, 0, 0), new Vector3(-1 * ratio, 0, 0), new Vector3(1 * ratio, 0, 0), Vector3.zero));
        gameQuestions.Add(new GameQuestion(3, "Vous allez très bien financièrement. Vous refléchissez à quoi faire de votre argent.", "Faudrait que je payes mes impôts.", "Une résidence secondaire à la mer / montagne", "Des putes et de la coke", "", new Vector3(1 * ratio, 0, 0), new Vector3(-1 * ratio, 0 * ratio, 1 * ratio), new Vector3(-1 * ratio, 1 * ratio, 0), Vector3.zero));
        gameQuestions.Add(new GameQuestion(3, "Un ami se retrouve à decouvert et vous demande de l'aide. Vous lui conseillez :", "Des pâtes au beurre", "Des pâtes au jambon", "Un emprunt pour aller au Burger King", "", new Vector3(0, 0, -1 * ratio), new Vector3(0 * ratio, 0 * ratio, -1 * ratio), new Vector3(0 * ratio, 0 * ratio, 1 * ratio), Vector3.zero));
    }

    void doGameQuestions()
    {
        GameQuestion tmp;

        next_question = false;
        int i = Random.Range(0, gameQuestions.Count);
        tmp = gameQuestions[i];

        UIObjs.instance.question.GetComponent<Text>().text = tmp.question;

        UIObjs.instance.answer1_obj.SetActive(true);
        UIObjs.instance.answer2_obj.SetActive(true);
        UIObjs.instance.answer3_obj.SetActive(false);
        UIObjs.instance.answer4_obj.SetActive(false);

        if (tmp.ansNbr >= 3)
            UIObjs.instance.answer3_obj.SetActive(true);
        if (tmp.ansNbr == 4)
            UIObjs.instance.answer4_obj.SetActive(true);

        bool a1 = false;
        bool a2 = false;
        bool a3 = false;
        bool a4 = false;
        foreach (string question in tmp.answers.Keys)
        {
            int y = Random.Range(0, tmp.ansNbr);
            while ((y == 0 && a1) || (y == 1 && a2) || (y == 2 && a3) || (y == 3 && a4))
            {
                y = Random.Range(0, tmp.ansNbr);
            }

            if (y == 0)
            {
                a1 = true;
                UIObjs.instance.answer1.GetComponent<Text>().text = question;
                UIObjs.instance.answer1_obj.GetComponent<ButtonData>().setInfluenceData(tmp.answers[question]);
            }
            else if (y == 1)
            {
                a2 = true;
                UIObjs.instance.answer2.GetComponent<Text>().text = question;
                UIObjs.instance.answer2_obj.GetComponent<ButtonData>().setInfluenceData(tmp.answers[question]);
            }
            else if (y == 2)
            {
                a3 = true;
                UIObjs.instance.answer3.GetComponent<Text>().text = question;
                UIObjs.instance.answer3_obj.GetComponent<ButtonData>().setInfluenceData(tmp.answers[question]);
            }
            else if (y == 3)
            {
                a4 = true;
                UIObjs.instance.answer4.GetComponent<Text>().text = question;
                UIObjs.instance.answer4_obj.GetComponent<ButtonData>().setInfluenceData(tmp.answers[question]);
            }
        }

        StartCoroutine(wait_for_response());
    }

    IEnumerator wait_for_response()
    {
        while (!next_question)
            yield return null;
        if (!finish())
            doGameQuestions();
    }

    public void ChangeInfluence(Vector3 data)
    {
        farmersInfluence += (int)(data.x * peoplemult);
        militaryInfluence += (int)(data.y * militarymult);
        banksInfluence += (int)(data.z * bankmult);

        if (farmersInfluence < 0)
            farmersInfluence = 0;
        if (militaryInfluence < 0)
            militaryInfluence = 0;
        if (banksInfluence < 0)
            banksInfluence = 0;

        if (farmersInfluence > 100)
            farmersInfluence = 100;
        if (militaryInfluence > 100)
            militaryInfluence = 100;
        if (banksInfluence > 100)
            banksInfluence = 100;

        globalInfluence = (farmersInfluence + militaryInfluence + banksInfluence) / 3;

        if (game)
        {
            UIObjs.instance.armyFill.GetComponent<Image>().fillAmount = (float)militaryInfluence / 100;
            UIObjs.instance.bankFill.GetComponent<Image>().fillAmount = (float)banksInfluence / 100;
            UIObjs.instance.peopleFill.GetComponent<Image>().fillAmount = (float)farmersInfluence / 100;

            if (globalInfluence <= 40)
            {
                UIObjs.instance.peopleNormal.SetActive(false);
                UIObjs.instance.peopleHappy.SetActive(false);
                UIObjs.instance.peopleUnhappy.SetActive(true);
            }
            else if (globalInfluence >= 60)
            {
                UIObjs.instance.peopleNormal.SetActive(false);
                UIObjs.instance.peopleHappy.SetActive(true);
                UIObjs.instance.peopleUnhappy.SetActive(false);
            }
            else
            {
                UIObjs.instance.peopleNormal.SetActive(true);
                UIObjs.instance.peopleHappy.SetActive(false);
                UIObjs.instance.peopleUnhappy.SetActive(false);
            }
        }

        next_question = true;
    }

    public void ChangeCaracs(Vector3 caracs)
    {
        charisma += (int)caracs.x;
        niceBoiiii += (int)caracs.y;
        generosity += (int)caracs.z;

        if (charisma <= 0)
            charisma = 1;
        if (niceBoiiii <= 0)
            niceBoiiii = 1;
        if (generosity <= 0)
            generosity = 1;
    }

    bool    finish()
    {
        if (militaryInfluence < 25 || farmersInfluence < 25 || banksInfluence < 25)
        {
            badfinish();
            return true;
        }
        else if (globalInfluence > 85)
        {
            goodfinish();
            return true;
        }
        return false;
    }

    void    badfinish()
    {
        UIObjs.instance.answer1_obj.SetActive(false);
        UIObjs.instance.answer2_obj.SetActive(false);
        UIObjs.instance.answer3_obj.SetActive(false);
        UIObjs.instance.answer4_obj.SetActive(false);
        if (militaryInfluence <= farmersInfluence && militaryInfluence <= banksInfluence)
            UIObjs.instance.question.GetComponent<Text>().text = "Vos forces se retournent contre vous, vous n'avez pas sû gérer la puissance qui vous entourait et vous n'avez plus que vos billets pour pleurer, du moins, jusqu'à ce que la morgue vous embarque.";
        else if (farmersInfluence <= militaryInfluence && farmersInfluence <= banksInfluence)
            UIObjs.instance.question.GetComponent<Text>().text = "La population vous déteste et se retourne contre vous, comme Louis XVI vous finissez sur la grande place face à vos forces militaires et votre richesse, bien sûr, comme lui, votre tête fini loin du tronc.";
        else if (banksInfluence <= militaryInfluence && banksInfluence <= militaryInfluence)
            UIObjs.instance.question.GetComponent<Text>().text = "Vous vous rendez compte que vous n'avez plus d'argent, le Qatar rachète votre pays, votre population cherche à vous sauvez, mais, excusez moi, entre plusieurs millions de dollars et votre vie, le choix fût vite fait.";
    }

    void    goodfinish()
    {
        UIObjs.instance.answer1_obj.SetActive(false);
        UIObjs.instance.answer2_obj.SetActive(false);
        UIObjs.instance.answer3_obj.SetActive(false);
        UIObjs.instance.answer4_obj.SetActive(false);
        if (militaryInfluence >= farmersInfluence && militaryInfluence >= banksInfluence)
            UIObjs.instance.question.GetComponent<Text>().text = "Vous avez sû vous faire aimer de tous, mais bien sûr, votre armée vous a un peu aidée en assassinant tous vos opposants, bravo, vous êtes la dernière civilisation de ce monde !";
        else if (farmersInfluence >= militaryInfluence && farmersInfluence >= banksInfluence)
            UIObjs.instance.question.GetComponent<Text>().text = "Votre population vous adore jusqu'à créer une chaine de téléréalité simplement pour suivre votre vie, les populations voisines vous idolâtre jusqu'à ce que vous deveniez la seule nation peuplée du monde, bravo, vous avez ruinez tout le monde, sauf vous !";
        else if (banksInfluence >= militaryInfluence && banksInfluence >= farmersInfluence)
            UIObjs.instance.question.GetComponent<Text>().text = "Vous êtes tellement riche que toutes les banques mondiales investissent chez vous, votre argent vous permets même de racheter la planète entière et les planètes voisines ! Vous êtes le maitre de l'univers, mais tout le monde devient riche, l'économie s'arrête, plus rien n'est à faire !";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}

namespace PO
{
    public enum AlgorithmType
    {
        Bolha,
        Bobo,
        Insercao,
        Selecao,
        Shell,
        Quick,
        Heap,
    }

    public enum DataEntryType
    {
        Aleatorio,
        Ordenado,
        Invertido,
    }   

    static class Options
    {
        public static int ScreenWidth = 800;//1024;
        public static int ScreenHeight = 600;//768;
        public static bool FullScreen = false;
        public static AlgorithmType AlgorithmType = AlgorithmType.Bolha;
        public static DataEntryType DataEntryType = DataEntryType.Aleatorio;
        public static int[] DataEntrySizes = { 5, 10, 50, 100, 200 };
        public static int DataEntrySize = 1;

        public static float acceleration = 0.5f;
        public static int maxVelocity = 30;
        public static int easingDistance = 10;

        public static int intervalo = 1;
        //public static int signboardTimer = 60;
        public static int signFontSize = 18;//Adicionar 4 ao tamanho real, para dar espaçamento

        public const byte unselectedBarAlpha = (byte)255;
        public const int barBorderWidth = 1;
        public const int maxValue = 10000;
        public const float safeBarHeight = 0.8f;
        public const float listenerOffset = 0.02f;

        public const int barPositionY = 50;

        public static System.Collections.Generic.List<string> creditos
        {
            get
            {
                System.Collections.Generic.List<string> credits = new System.Collections.Generic.List<string>();

                credits.Add("Programa feito por Code59, em 2010");
                credits.Add("Arquitetura: Hallen Fred e Rafael Hrasko");
                credits.Add("Thread control: Hallen Fred");
                credits.Add("Algoritmos: Viktor");
                credits.Add("Front-End: Rafael Hrasko e Eduardo Araujo");
                credits.Add("Agradecemos a Professora Chintia, que fez o pedido desse software");

                return credits;
            }
        }

        public static System.Collections.Generic.List<string> help
        {
            get
            {
                System.Collections.Generic.List<string> help = new System.Collections.Generic.List<string>();

                help.Add("Para dar um passo, aperte a tecla Espaco");
                help.Add("Para rodar o algoritmo sem parar, tecle Enter");
                help.Add("Para voltar ao menu, aperte a tecla Esc");

                return help;
            }
        }
            
    }
}

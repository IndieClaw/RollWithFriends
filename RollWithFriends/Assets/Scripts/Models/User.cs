namespace Models
{

    public class User : BaseEntity
    {

        public string Name { get; set; }


        public User()
        {

        }

        public User(string name, string unityCustomTokenAPI)
        {
            Name = name;
            UnityCustomTokenAPI = unityCustomTokenAPI;
        }
    }
}
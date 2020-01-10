namespace Models
{

    public class User : BaseEntity
    {

        public string Name { get; set; }


        public User()
        {

        }

        public User(string id, string name, string unityCustomTokenAPI)
        {
            Id = id;
            Name = name;
            UnityCustomTokenAPI = unityCustomTokenAPI;
        }
    }
}
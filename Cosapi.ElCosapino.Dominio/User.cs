namespace Cosapi.ElCosapino.Dominio
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
    }
}

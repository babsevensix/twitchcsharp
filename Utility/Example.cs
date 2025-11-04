// public class StringBox
// {
//     private string _value;

//     public void SetValue(string value) => _value = value;

//     public string GetValue() => _value;

// }

// public class IntBox
// {
//     private int _value;

//     public void SetValue(int value) => _value = value;

//     public int GetValue() => _value;

// }

// public interface IEntity
// {
//     public int Id { get; set; }
// }

// public class Box<T> where T : IEntity, new()
// {
//     private T _value;

//     public void SetValue(T value) => _value = value;

//     public T GetValue() => _value;

//     public T CreateNewValue()
//     {
//         return new T();
//     }
// }

// public class EsempioUtilizzo
// {
//     public EsempioUtilizzo()
//     {
//         /*IntBox b = new IntBox();
//         b.SetValue(1);

//         StringBox sb = new StringBox();
//         b.SetValue("Pippo");*/

//         /*Box<int> b = new Box<int>();
//         b.SetValue(1);

//         Box<string> sb = new Box<string>();
//         sb.SetValue("Pippo");*/

//         Box<PersonaEntity> pb = new Box<PersonaEntity>();
//         //pb.SetValue(new PersonaEntity());
//         PersonaEntity pe = pb.CreateNewValue();

//     }
// }


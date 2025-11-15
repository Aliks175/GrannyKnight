using UnityEditor;

[CustomEditor(typeof(Interacteble), true)]
public class InteractebleEditor : Editor
{
    
    public override void OnInspectorGUI()// Вызывается каждый раз когда юнити обновляет интерфейс редактора 
    {
        Interacteble interacteble = (Interacteble)target;// тот самый скрипт который выбран в редакторе  

        if (target.GetType() == typeof(InteractebleOnlyEvents))// мы проверяем является ли наш экземпляр Interacteble = InteractebleOnlyEvents
        {
            // здесь мы пишем полностью новый редактор с полями 
            interacteble.Description = EditorGUILayout.TextField("Prompt", interacteble.Description);// мы создаем поле в редакторе с названием Prompt
                                                                                                     // , оно будет присваивать значение для interacteble.Prompt
            EditorGUILayout.HelpBox("InteractebleOnlyEvents Can only Use UnityEvents", MessageType.Info);// мы создаем информационную плашку
                                                                                                         // с введеным текстом 
            if (interacteble.GetComponent<InteractebleEvents>() == null)// проверяем есть ли на объекте InteractebleEvents
            {
                interacteble.UseEvents = true;
                interacteble.gameObject.AddComponent<InteractebleEvents>();
            }
        }
        else// если нет то мы используем редактор по умолчанию
        {
            base.OnInspectorGUI();// это реализует наш Editor по умолчанию 

            if (interacteble.UseEvents)//проверка будем ли мы добавлять ивенты в редакторе 
            {
                if (interacteble.GetComponent<InteractebleEvents>() == null) // мы проверяем весит ли уже класс с ивентом ? 
                {
                    interacteble.gameObject.AddComponent<InteractebleEvents>();// если нет то добавляем его 
                }
            }
            else// если после проверки решили что мы не используем ивенты
            {
                if (interacteble.GetComponent<InteractebleEvents>() != null)// если они весят на объекте 
                {
                    DestroyImmediate(interacteble.GetComponent<InteractebleEvents>());// то мы их удаляем 
                }
            }
        }
    }
}
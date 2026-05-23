using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Data.Save;
    using Enumerations;
    using Unity.VisualScripting;
    using Utilities;
    using Windows;

    public class DSMultipleChoiceNode : DSNode
    {
        public override void Initialize(string nodeName, DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(nodeName, dsGraphView, position);
            DialogueType = DSDialogueType.MultipleChoice;

            DSChoiceSaveData choiceData = new() { Text = "New Choice" };
            Choices.Add(choiceData);
        }

        public override void Draw()
        {
            base.Draw();

            Button addChoiceButton = DSElementUtility.CreateButton(
                "Add Choice",
                () =>
                {
                    DSChoiceSaveData choiceData = new() { Text = "New Choice" };
                    Choices.Add(choiceData);

                    Port choicePort = CreateChoicePort(choiceData);

                    outputContainer.Add(choicePort);

                    RefreshExpandedState();
                }
            );

            addChoiceButton.AddToClassList("ds-node__button");
            mainContainer.Insert(1, addChoiceButton);

            foreach (DSChoiceSaveData choice in Choices)
            {
                Port choicePort = CreateChoicePort(choice);

                outputContainer.Add(choicePort);
            }

            RefreshExpandedState();
        }

        private Port CreateChoicePort(object userData)
        {
            Port choicePort = this.CreatePort();
            choicePort.userData = userData;

            DSChoiceSaveData choiceData = (DSChoiceSaveData)userData;

            Toggle checkBox = DSElementUtility.CreateCheckbox();
            checkBox.value = choiceData.Visible;
            checkBox.RegisterValueChangedCallback(evt =>
            {
                choiceData.Visible = evt.newValue;
            });

            Button deleteChoiceButton = DSElementUtility.CreateButton(
                "X",
                () =>
                {
                    if (Choices.Count == 1)
                        return;

                    if (choicePort.connected)
                        graphView.DeleteElements(choicePort.connections);

                    Choices.Remove(choiceData);

                    graphView.RemoveElement(choicePort);
                    outputContainer.Remove(checkBox);

                    RefreshExpandedState();
                }
            );

            deleteChoiceButton.AddToClassList("ds-node__button");

            TextField choiceTextField = DSElementUtility.CreateTextField(
                choiceData.Text,
                null,
                callback =>
                {
                    choiceData.Text = callback.newValue;
                }
            );

            choiceTextField.AddClasses(
                "ds-node__text-field",
                "ds-node__text-field__hidden",
                "ds-node__choice-text-field"
            );

            choicePort.Add(choiceTextField);
            choicePort.Add(deleteChoiceButton);

            outputContainer.Add(choicePort);
            outputContainer.Add(checkBox);

            return choicePort;
        }
    }
}

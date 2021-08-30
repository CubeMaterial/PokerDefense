using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

// public class EnemyWindowEditor : EditorWindow
// {

//     SerializedObject EnemyDataListSerialized;

//     private static EnemyDataList list;

//     private int iSelectedMode;
//     private int iSelectedItemIndex;

//     static string EnemyDataListPath = @"Assets/Data/EnemyDataList.asset";

//     Vector2 buttonAreaScrollPos = Vector2.zero;
//     Vector2 itemAreaScrollPos = Vector2.zero;

//     string objectToFind = string.Empty;

//     bool itemWasDeleted = false;
//     bool showItemPositions = true;

//     private int listSize;

//     [MenuItem("TD/Enemy Data Window")]
//     static void OpenWindow()
//     {
//         if (!AssetDatabase.LoadAssetAtPath(EnemyDataListPath, typeof(EnemyDataList)))
//         {
//             list = CreateInstance<EnemyDataList>();
//             AssetDatabase.CreateAsset(list, EnemyDataListPath);
//             AssetDatabase.SaveAssets();
//             AssetDatabase.Refresh();
//         }

//         EnemyWindowEditor window = (EnemyWindowEditor)GetWindow(typeof(EnemyWindowEditor));


//         window.minSize = new Vector2(1000, 600);
//         window.Show();
//     }

//     void CreateItemDatabase()
//     {

//     }

//     private void OnEnable()
//     {
//         list = (EnemyDataList)AssetDatabase.LoadAssetAtPath(EnemyDataListPath, typeof(EnemyDataList));
//         EnemyDataListSerialized = new SerializedObject(list);
//     }

//     private void OnGUI()
//     {
//         DrawLayouts();

//     }

//     private void DrawLayouts()
//     {

//         EnemyDataListSerialized.Update();

//         EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
//         DrawSideArea();
//         DrawItemArea();
//         EditorGUILayout.EndHorizontal();

//         EnemyDataListSerialized.ApplyModifiedProperties();

//     }

//     void DrawSideArea()
//     {
//         listSize = list.mEnemyDataList.Count;

//         EditorGUILayout.BeginVertical("Box", GUILayout.Width(325)); //Make vertical area for side view

//         //Category and item count
//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("Total Item : " + listSize, EditorStyles.label);
//         EditorGUILayout.EndHorizontal();

//         //Make it scrollable
//         buttonAreaScrollPos = EditorGUILayout.BeginScrollView(buttonAreaScrollPos, false, false, GUILayout.ExpandWidth(true));

//         //If list has something
//         if (listSize > 0)
//         {
//             EditorGUILayout.Space();

//             //Loop and show all buttons in page
//             for (int elementIndex = 0; elementIndex < listSize; elementIndex++)
//             {
//                 EditorGUILayout.BeginHorizontal("Box");
//                 EnemyData item = GetEnemyDataAtIndex(elementIndex);
//                 if (!item.mEnemyCode.ToLower().Contains(objectToFind.ToLower()))
//                     continue;

//                 GUILayout.Label((elementIndex + 1).ToString(), GUILayout.MaxWidth(32));

//                 //Draw mini buttons near item names
//                 MiniSideButtonsControl(elementIndex);

//                 //If the item was deleted then stop here
//                 if (!itemWasDeleted)
//                 {
//                     //Make selected button a different color
//                     if (elementIndex == iSelectedItemIndex)
//                         GUI.color = Color.yellow;

//                     //Side item button
//                     if (GUILayout.Button(item.mEnemyCode == string.Empty ? "Item Not Named" : item.mEnemyCode, GUILayout.MaxWidth(170)))
//                     {
//                         iSelectedItemIndex = elementIndex;
//                         GUIUtility.keyboardControl = 0;

//                         UnityEngine.Object Obj = list.mEnemyDataList[elementIndex];
//                         Obj.name = item.mEnemyCode;
//                     }
//                     GUI.color = Color.white;    //Reset color
//                 }

//                 EditorGUILayout.EndHorizontal();
//             }
//         }

//         EditorGUILayout.Space();
//         EditorGUILayout.EndScrollView();    //End here so the page buttons always stay where they are


//         //Add Item Button
//         GUI.color = Color.green;
//         if (GUILayout.Button("Add New Item"))
//             AddItem();

//         GUI.color = Color.white;
//         EditorGUILayout.EndVertical();
//     }

//     void DrawItemArea()
//     {
//         EditorGUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true));
//         itemAreaScrollPos = EditorGUILayout.BeginScrollView(itemAreaScrollPos, false, false);

//         //Draw item
//         if (iSelectedItemIndex != -1 && iSelectedItemIndex < listSize)
//         {
//             EnemyData nItem = GetEnemyDataAtIndex(iSelectedItemIndex);
//             if (nItem != null)
//                 DrawTowerDataSetting(nItem, iSelectedItemIndex);

//             //Update window
//             if (focusedWindow)
//                 focusedWindow.Repaint();
//         }
//         EditorGUILayout.EndScrollView();
//         EditorGUILayout.EndVertical();
//         EnemyDataListSerialized.Update();
//     }

//     private void DrawTowerDataSetting(EnemyData item, int index)
//     {
//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("타워 코드: ", GUILayout.Width(100));
//         item.mEnemyCode = EditorGUILayout.TextField(item.mEnemyCode);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();


//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("최소 공격력 : ", GUILayout.Width(100));
//         item.fLife = EditorGUILayout.FloatField(item.fLife);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("최대 공격력 : ", GUILayout.Width(100));
//         item.fSpeed = EditorGUILayout.FloatField(item.fSpeed);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("고정 공격력 : ", GUILayout.Width(100));
//         item.fAvoidRate = EditorGUILayout.FloatField(item.fAvoidRate);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("추가 공격력 : ", GUILayout.Width(100));
//         item.fFixedArmor = EditorGUILayout.FloatField(item.fFixedArmor);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("보스 공격력 : ", GUILayout.Width(100));
//         item.fReduceRate = EditorGUILayout.FloatField(item.fReduceRate);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("공격 속도 : ", GUILayout.Width(100));
//         item.iBuff = EditorGUILayout.IntField(item.iBuff);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         //EditorGUILayout.BeginHorizontal();
//         //GUI.color = Color.green;
//         //if (GUILayout.Button(new GUIContent("Add", "Add Drop Item"), EditorStyles.miniButton))
//         //{
//         //    item.mAnimalCodeList.Add(string.Empty);
//         //}
//         //GUI.color = Color.white;

//         //EditorGUILayout.EndHorizontal();
//         list.mEnemyDataList[index].name = item.mEnemyCode;
//         EnemyDataListSerialized.Update();
//     }


//     void MiniSideButtonsControl(int index)
//     {
//         itemWasDeleted = false;
//         //Remove button
//         if (GUILayout.Button(new GUIContent("-", "Delete Item"), EditorStyles.miniButtonLeft, GUILayout.Width(20)))
//         {
//             //If delete is confirmed then remove item entry and clear id
//             if (ItemRemovalConfirmed())
//             {
//                 Undo.RecordObject(list, "Delete Object");

//                 //database.DeleteID(item.itemCode);    //Delete ID
//                 GUIUtility.keyboardControl = 0;   //Remove control in case anything was selected
//                 DestroyImmediate(list.mEnemyDataList[index], true);
//                 list.mEnemyDataList[index] = null;
//                 list.mEnemyDataList.RemoveAt(index);

//                 AssetDatabase.SaveAssets();
//                 itemWasDeleted = true;
//                 EnemyDataListSerialized.Update();    //Update the serialized object to show changes

// #if !UNITY_5_3_OR_NEWER
//                  EditorUtility.SetDirty(list);
// #endif
//             }
//         }

//         //Duplicate button
//         else if (GUILayout.Button(new GUIContent("+", "Duplicate Item"), EditorStyles.miniButtonRight, GUILayout.Width(20)))
//         {
//             Undo.RecordObject(list, "Duplicate Object");
//             EnemyData item = CreateInstance<EnemyData>();
//             EnemyData nItem = GetEnemyDataAtIndex(index);

//             item.mEnemyCode = nItem.mEnemyCode;
//             item.fLife = nItem.fLife;
//             item.fSpeed = nItem.fSpeed;
//             item.fAvoidRate = nItem.fAvoidRate;
//             item.fFixedArmor = nItem.fFixedArmor;
//             item.fReduceRate = nItem.fReduceRate;
//             item.iBuff = nItem.iBuff;

//             item.name = nItem.mEnemyCode;
//             list.mEnemyDataList.Add(item);
//             AssetDatabase.AddObjectToAsset(item, EnemyDataListPath);
//             EnemyDataListSerialized.Update();    //Update the serialized object to show these changes
//         }
//     }
//     void AddItem()
//     {
//         Undo.RecordObject(list, "Add Item");
//         EnemyData item = CreateInstance<EnemyData>();
//         item.mEnemyCode = list.mEnemyDataList.Count.ToString();
//         item.fLife = 0f;
//         item.fSpeed = 0f;
//         item.fAvoidRate = 0f;
//         item.fFixedArmor = 0f;
//         item.fReduceRate = 1f;
//         item.iBuff = 0;

//         item.name = item.mEnemyCode;
//         list.mEnemyDataList.Add(item);
//         AssetDatabase.AddObjectToAsset(item, EnemyDataListPath);
//         AssetDatabase.ImportAsset(EnemyDataListPath);
//         GUIUtility.keyboardControl = 0;
//         itemWasDeleted = true;
//         EnemyDataListSerialized.Update();

// #if !UNITY_5_3_OR_NEWER
//         EditorUtility.SetDirty(autoVidLists);
// #endif
//     }

//     EnemyData GetEnemyDataAtIndex(int index, bool mainOnly = false)
//     {
//         if (list.mEnemyDataList.Count > 0)
//             return list.mEnemyDataList[index];
//         else
//             return null;
//     }

//     bool ItemRemovalConfirmed()
//     {
//         return EditorUtility.DisplayDialog("Delete?", "Are you sure you want to remove this item?", "Yes", "No");
//     }
// }

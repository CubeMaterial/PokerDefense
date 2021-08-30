using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

// public class TowerWindowEditor : EditorWindow
// {

//     SerializedObject EnemyDataListSerialized;

//     private static TowerDataList list;

//     private int iSelectedMode;
//     private int iSelectedItemIndex;

//     static string TowerDataListPath = @"Assets/Data/TowerDataList.asset";

//     Vector2 buttonAreaScrollPos = Vector2.zero;
//     Vector2 itemAreaScrollPos = Vector2.zero;

//     string objectToFind = string.Empty;

//     bool itemWasDeleted = false;
//     bool showItemPositions = true;

//     private int listSize;

//     [MenuItem("TD/Tower Data Window")]
//     static void OpenWindow()
//     {
//         if (!AssetDatabase.LoadAssetAtPath(TowerDataListPath, typeof(TowerDataList)))
//         {
//             list = CreateInstance<TowerDataList>();
//             AssetDatabase.CreateAsset(list, TowerDataListPath);
//             AssetDatabase.SaveAssets();
//             AssetDatabase.Refresh();
//         }

//         TowerWindowEditor window = (TowerWindowEditor)GetWindow(typeof(TowerWindowEditor));


//         window.minSize = new Vector2(1000, 600);
//         window.Show();
//     }

//     void CreateItemDatabase()
//     {

//     }

//     private void OnEnable()
//     {
//         list = (TowerDataList)AssetDatabase.LoadAssetAtPath(TowerDataListPath, typeof(TowerDataList));
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
//         listSize = list.mTowerDataList.Count;
       
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
//                 TowerData item = GetTowerDataAtIndex(elementIndex);
//                 if (!item.mTowerCode.ToLower().Contains(objectToFind.ToLower()))
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
//                     if (GUILayout.Button(item.mTowerCode == string.Empty ? "Item Not Named" : item.mTowerCode, GUILayout.MaxWidth(170)))
//                     {
//                         iSelectedItemIndex = elementIndex;
//                         GUIUtility.keyboardControl = 0;

//                         UnityEngine.Object Obj = list.mTowerDataList[elementIndex];
//                         Obj.name = item.mTowerCode;
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
//             TowerData nItem = GetTowerDataAtIndex(iSelectedItemIndex);
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

//     private void DrawTowerDataSetting(TowerData item, int index)
//     {
//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("타워 코드: ", GUILayout.Width(100));
//         item.mTowerCode = EditorGUILayout.TextField(item.mTowerCode);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();


//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("최소 공격력 : ", GUILayout.Width(100));
//         item.fMinAttackPoint = EditorGUILayout.FloatField(item.fMinAttackPoint);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("최대 공격력 : ", GUILayout.Width(100));
//         item.fMaxAttackPoint = EditorGUILayout.FloatField(item.fMaxAttackPoint);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("고정 공격력 : ", GUILayout.Width(100));
//         item.fFixedAttackPoint = EditorGUILayout.FloatField(item.fFixedAttackPoint);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("추가 공격력 : ", GUILayout.Width(100));
//         item.fAddAttackPoint = EditorGUILayout.FloatField(item.fAddAttackPoint);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("보스 공격력 : ", GUILayout.Width(100));
//         item.fBossAttackPoint = EditorGUILayout.FloatField(item.fBossAttackPoint);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("공격 속도 : ", GUILayout.Width(100));
//         item.fAttackDelay = EditorGUILayout.FloatField(item.fAttackDelay);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("크리티컬 확률 : ", GUILayout.Width(100));
//         item.fCriticalRate = EditorGUILayout.FloatField(item.fCriticalRate);
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("크리티컬 배율 : ", GUILayout.Width(100));
//         item.fCriticalFactor = EditorGUILayout.FloatField(item.fCriticalFactor);
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
//         list.mTowerDataList[index].name = item.mTowerCode;
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
//                 DestroyImmediate(list.mTowerDataList[index], true);
//                 list.mTowerDataList[index] = null;
//                 list.mTowerDataList.RemoveAt(index);
                
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
//             TowerData item = CreateInstance<TowerData>();
//             TowerData nItem = GetTowerDataAtIndex(index);

//             item.mTowerCode = nItem.mTowerCode;
//             item.fMinAttackPoint = nItem.fMinAttackPoint;
//             item.fMaxAttackPoint = nItem.fMaxAttackPoint;
//             item.fAttackDelay = nItem.fAttackDelay;
//             item.fFixedAttackPoint = nItem.fFixedAttackPoint;
//             item.fAddAttackPoint = nItem.fAddAttackPoint;
//             item.fBossAttackPoint = nItem.fBossAttackPoint;
//             item.fCriticalRate = nItem.fCriticalRate;
//             item.fCriticalFactor = nItem.fCriticalFactor;

//             item.name = nItem.mTowerCode;
//             list.mTowerDataList.Add(item);
//             AssetDatabase.AddObjectToAsset(item, TowerDataListPath);
           
//             EnemyDataListSerialized.Update();    //Update the serialized object to show these changes
//         }
//     }
//     void AddItem()
//     {
//         Undo.RecordObject(list, "Add Item");
//         TowerData item = CreateInstance<TowerData>();
//         item.mTowerCode = list.mTowerDataList.Count.ToString();
//         item.fAddAttackPoint = 0f;
//         item.fFixedAttackPoint = 0f;
//         item.fAddAttackPoint = 0f;
//         item.fBossAttackPoint = 0f;
//         item.fAttackDelay = 1f;
//         item.fCriticalRate = 0.1f;
//         item.fCriticalFactor = 1.5f;

//         item.name = item.mTowerCode;
//         list.mTowerDataList.Add(item);
//         AssetDatabase.AddObjectToAsset(item, TowerDataListPath);
//         AssetDatabase.ImportAsset(TowerDataListPath);
//         GUIUtility.keyboardControl = 0;
//         itemWasDeleted = true;
//         EnemyDataListSerialized.Update();

// #if !UNITY_5_3_OR_NEWER
//         EditorUtility.SetDirty(autoVidLists);
// #endif
//     }

//     TowerData GetTowerDataAtIndex(int index, bool mainOnly = false)
//     {
//         if (list.mTowerDataList.Count > 0)
//             return list.mTowerDataList[index];
//         else
//             return null;
//     }

//     bool ItemRemovalConfirmed()
//     {
//         return EditorUtility.DisplayDialog("Delete?", "Are you sure you want to remove this item?", "Yes", "No");
//     }
// }

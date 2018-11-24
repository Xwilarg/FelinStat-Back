## FelinStats-Back
Backend for a french hackaton "Armées Du Futur"<br/><br/>

The goal was the detect the wear of weapons given some datas by the army.<br/>
We are however not allow to share them publicy.<br/><br/>

However, here are the format of the files that need to be near the executable:<br/>
#### mttr.txt:
weaponTimeofRepair;numberOfWeapons

#### mtbf.txt:
weaponId;weaponMaintenanceDate(dd/MM/yyyy)

#### serviceTime.txt:
weaponId;weaponMaintenanceDate(dd/MM/yyyy)<br/>
(We used 2 differents files because we identified 2 ids)

#### weapon.txt:
weaponId;applicant;repairor;interventionLevel;itType

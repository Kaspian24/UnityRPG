EXTERNAL startQuest(questId)
EXTERNAL isQuestStartable(questId)
EXTERNAL triggerEvent(npcName, conversationTopic)

{isQuestStartable("Quest4"):
~startQuest("Quest4")
}

Pierwsza linia

Druga linia. Czy chcesz wyświetlać kolejne linie?
+ Tak
-> kontynuuj
+ Nie
-> DONE
+ Tak ale jako bardzo długa opcja dialogowa bo chcę sprawdzić czy to się wyświetla prawidłowo.
-> kontynuuj
+ Nie, a te przyciski to działają?
-> nieWiem
== kontynuuj ==
Długa trzecia linia, jest tu dużo więcej tekstu niż w pozostałych liniach. Ciekawe jak to będzie działało.

Czwarta linia
Piąta linia
Szósta linia
Siódma linia
Ósma linia
Dziewiąta linia
Dziesiąta linia
-> DONE

== nieWiem ==
Nie wiem
-> DONE
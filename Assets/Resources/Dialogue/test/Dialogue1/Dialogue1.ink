EXTERNAL startQuest(questId)
EXTERNAL isQuestStartable(questId)
EXTERNAL triggerEvent(npcName, conversationTopic)

{ isQuestStartable("Quest1"):
-> Quest1Startable
}
Nie mamy o czym rozmawiać.
-> DONE

== Quest1Startable ==
Witaj przybyszu!
Potrzebujemy twojej pomocy z odparciem najazdu czerwonych. Czy podejmujesz się wyzwania?
+ tak
~startQuest("Quest1")
Świetnie!
-> DONE
+ nie
Szkoda
-> DONE
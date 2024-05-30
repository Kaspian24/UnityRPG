INCLUDE ../../globals.ink

VAR zakonczonoZadanie = false
~ zakonczonoZadanie = isDialogueStartable("NPC1", "zakonczonoZadanie")

VAR quest1 = false
~ quest1 = isQuestStartable("Quest1")

{ zakonczonoZadanie:
-> Quest1Task2Finish
}

{ quest1:
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

== Quest1Task2Finish ==
~topicTalkedAbout("NPC1", "zakonczonoZadanie")
~disableTopic("NPC1", "zakonczonoZadanie")
Dziękuję za pomoc, bez ciebie by się nie udało.
-> DONE
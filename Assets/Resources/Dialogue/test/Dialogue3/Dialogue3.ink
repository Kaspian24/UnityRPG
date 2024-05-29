INCLUDE ../../globals.ink

{ isQuestStartable("Quest3"):
-> Quest3Startable
}
Nie mamy o czym rozmawiać.
-> DONE

== Quest3Startable ==
Witaj przybyszu!
Potrzebujemy twojej pomocy z odparciem najazdu czerwonych. Czy podejmujesz się wyzwania?
+ tak
~startQuest("Quest3")
Świetnie!
-> DONE
+ nie
Szkoda
-> DONE
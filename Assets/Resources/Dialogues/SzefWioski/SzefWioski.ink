INCLUDE ../globals.ink

VAR wezwanie = false
~ wezwanie = isDialogueStartable("Szef", "Wezwanie")

VAR szkielety = false
~ szkielety = isDialogueStartable("Szef", "Szkielety")

{ wezwanie:
-> DialogWezwanie
}

{ szkielety:
-> DialogSzkielety
}

Nie mamy o czym rozmawiać.
-> DONE

== DialogWezwanie ==
Naszą wioskę spotyka ostatnio wiele nieszczęść! Najpierw napadały nas szkielety a teraz jeszcze smok!
Czy zechcesz udzielić nam pomocy w obronie naszej wioski?
+ tak
~startQuest("Szkielety")
-> WezwanieZgoda
+ nie
Wróc jeżeli zmienisz zdanie.
-> DONE

== WezwanieZgoda ==
~topicTalkedAbout("Szef", "Wezwanie")
~disableTopic("Szef", "Wezwanie")
Dziękujemy!
W pierwszej kolejności postaraj się pokonać chociaż część z pobliskich szkieletów.
Najbliższy oddział znajduje się za lasem za moim domem.
-> DONE

== DialogSzkielety ==
Widzę że udało Ci się uporać z kilkoma szkieletami.
Myślę jednak, że powinieneś zdobyć więcej doświadczenia nim zawalczysz ze zmokiem.
Nie będę Cię jednak powstrzymywał, smok znajduje się na łące za pojedynczym domem w pobliżu jeziora.
Powodzenia!
~topicTalkedAbout("Szef", "Szkielety")
~disableTopic("Szef", "Szkielety")
~startQuest("Smok")
-> DONE
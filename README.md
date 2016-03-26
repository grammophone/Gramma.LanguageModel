# Gramma.LanguageModel
This library abstracts a language in order to be consumed by [Gramma.Inference](https://github.com/grammophone/Gramma.Inference), a part-of-speech tagging and lemmatization framework.

It defines the abstract class `LanguageProvider`, a contract for providing resources for processing text of a language. This is a root object for providing several aspects of the language. First, it should provide its grammar model, as shown in the UML below:

![Grammar model](http://s29.postimg.org/r3wzmc3d3/Grammar_model.png)

`LanguageProvider` derivations should also provide a `SentenceBreaker` implementation which instructs the system how to separate sentences and the words in them, plus a `Syllabizer` implementation which brings words to the syllabic representation required by the system in order to facilitate machine learning of grammatical features as well as providing distance metrics between syllables, as shown in the following UML diagram:

![Sentence, syllabic representation and handling](http://s27.postimg.org/kn7uwxdur/Sentence_and_syllabic_handling.png)

This project depends on the following projects residing in sibling directories:
* [Gramma.GenericContentModel](https://github.com/grammophone/Gramma.GenericContentModel)
* [Gramma.Linq](https://github.com/grammophone/Gramma.Linq)


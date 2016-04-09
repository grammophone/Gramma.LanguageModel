# Grammophone.LanguageModel
This library abstracts a language in order to be consumed by [Grammophone.EnnounInference](https://github.com/grammophone/Grammophone.EnnounInference), a part-of-speech tagging and lemmatization framework.

It defines the abstract class `LanguageProvider`, a contract for providing resources for processing text of a language. This is a root object for providing several aspects of the language. First, it should provide its grammar model, as shown in the UML below:

![Grammar model](http://s29.postimg.org/r3wzmc3d3/Grammar_model.png)

`LanguageProvider` derivations should also provide a `SentenceBreaker` implementation which instructs the system how to separate sentences and the words in them, plus a `Syllabizer` implementation which brings words to the syllabic representation required by the system in order to facilitate machine learning of grammatical features as well as providing distance metrics between syllables, as shown in the following UML diagram:

![Sentence, syllabic representation and handling](http://s27.postimg.org/kn7uwxdur/Sentence_and_syllabic_handling.png)

This library also defines the contract for training sources for a language. As shown in the following diagram, all kinds of sources derive from `TrainingSource<T>`, where `T` is the type of item in the stream of training data. In this way, the `TaggedWordTrainingSource` is a `TrainingSource<TaggedWordForm>` and `SentenceTrainingSource` is a `TrainingSource<TaggedSentence>`. Training sources can be combined via `CompositeTrainingSource<T>` and automatically sliced by `NFoldTrainingSource<T>` to support n-fold validation.

![Training sources contract](http://s12.postimg.org/95bu0xgwt/Training_sources_contract.png)

This project depends on the following projects residing in sibling directories:
* [Grammophone.GenericContentModel](https://github.com/grammophone/Grammophone.GenericContentModel)
* [Grammophone.Linq](https://github.com/grammophone/Grammophone.Linq)

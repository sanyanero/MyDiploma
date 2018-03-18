import { LanguageType } from "../_enums/languageType";

export class Question {
  id: string;
  questionText: string;
  language: LanguageType;
  option1: string;
  option2: string;
  option3: string;
  option4: string;

  constructor(id?: string, questionText?: string, language?: LanguageType, option1?: string, option2?: string, option3?: string, option4?: string) {
    this.id = id;
    this.questionText = questionText;
    this.language = language;
    this.option1 = option1;
    this.option2 = option2;
    this.option3 = option3;
    this.option4 = option4;
  }
}

import { User } from './user.model';
import { Answer } from './answer.model';

export class Question {
    questionID: number;
    title: string;
    description: string;
    answerCount: number;
    createdAt: Date;
    updatedAt: Date;
    author: User;
    answers: Array<Answer>;
}

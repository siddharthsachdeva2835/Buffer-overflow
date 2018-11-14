import { User } from './user.model';
export class Answer {
    answerID: number;
    body: string;
    createdAt: Date;
    updatedAt: Date;
    author: User;
    questionID: number;
    votings: [{status: boolean, userID: string}];
}

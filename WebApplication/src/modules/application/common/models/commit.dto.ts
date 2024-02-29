import { CommitState } from "../enums/commit-state.enum";

export class CommitDto {
  id: number;
  commitState: CommitState;
  rootId: number;
  hasHistory: boolean | null;
  changeStateDescription: string;
}
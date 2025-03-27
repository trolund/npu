import { ScoreSummeryResponse } from "./ScoreSummeryResponse";

export interface NpuResponse {
  id: string;
  name: string;
  description?: string;
  images: string[];
  score?: ScoreSummeryResponse;
}

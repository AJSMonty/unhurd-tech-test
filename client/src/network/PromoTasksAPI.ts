import axios from 'axios';

import { CreatePromoTask, PromoTask } from '@/models/PromoTaskModel';

class PromoTaskAPI {
  static getAllPromoTasksFromAccountId = async ({ accountId }: { accountId?: string }) =>
    await axios.get<{ tasks: PromoTask[] }>(`/promo-tasks/${accountId}`);

  static createPromoTask = async ({ data }: { data: CreatePromoTask }) =>
    await axios.post<PromoTask>('/promo-task', data);

  static updatePromoTask = async ({ taskId, data }: { taskId: string; data: CreatePromoTask }) =>
    await axios.put<PromoTask>(`/promo-task/${taskId}`, data);
}

export default PromoTaskAPI;

export type PromoTask = {
  id: string;
  accountId: string;
  title: string;
  description: string;
  status: PromoTaskStatus;
  createdAt: string;
};

export type CreatePromoTask = {
  id?: string;
  accountId: string;
  title: string;
  description: string;
  status: PromoTaskStatus;
};

export type PromoTaskStatus = 'ToDo' | 'InProgress' | 'Done';

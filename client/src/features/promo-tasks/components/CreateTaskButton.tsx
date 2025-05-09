import { Button, Icon, Modal } from '@mui/material';
import useAllPromoTasks from '../hooks/useAllPromoTasks';
import PromoTaskAPI from '../../../network/PromoTasksAPI';
import useAccount from '../../../features/account/hooks/useAccount';
import { CreatePromoTask } from '../../../models/PromoTaskModel';
import { useMemo, useState } from 'react';
import { FormProvider, useForm } from 'react-hook-form';

const CreateTaskButton = () => {
  const { account } = useAccount();
  const { refetchPromoTasks } = useAllPromoTasks();

  const [openModal, setOpenModal] = useState(false);

  const defaultValues: CreatePromoTask = useMemo(
    () => ({
      accountId: account?.accountId,
      title: '',
      description: '',
      status: 'ToDo',
    }),
    [account?.accountId]
  );

  const formMethods = useForm<CreatePromoTask>({ defaultValues });
  const { register, handleSubmit, reset, watch } = formMethods;

  const title = watch('title');
  const description = watch('description');

  const isCreateDisabled = !title?.trim() || !description?.trim();

  const onSubmit = async (data: CreatePromoTask) => {
    try {
      await PromoTaskAPI.createPromoTask({ data });
      refetchPromoTasks();
      setOpenModal(false);
      reset();
    } catch (error) {
      console.error('Error creating task:', error);
    }
  };

  return (
    <>
      <Modal open={openModal} onClose={() => setOpenModal(false)}>
        <div className="create-task-modal">
          <h3>Create a new task</h3>
          <p className="text-faded small">Fill in the details below</p>
          <FormProvider {...formMethods}>
            <input
              className="mt10"
              type="text"
              placeholder="Enter title..."
              {...register('title', { required: true })}
            />
            <input
              className="mt10"
              type="text"
              placeholder="Enter description..."
              {...register('description', { required: true })}
            />

            <Button
              disabled={isCreateDisabled}
              className="btn-white m0 w100p mt20"
              type="submit"
              onClick={handleSubmit(onSubmit)}
            >
              Create
            </Button>
          </FormProvider>
        </div>
      </Modal>
      <Button className="btn-white" onClick={() => setOpenModal(true)}>
        <Icon className="pr8">add</Icon>
        Create task
      </Button>
    </>
  );
};

export default CreateTaskButton;

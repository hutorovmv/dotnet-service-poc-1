import { Meta, StoryObj } from '@storybook/angular';
import { EmailInput } from '../../../app/components/common/email-input/email-input';

const meta: Meta<EmailInput> = {
  title: 'Common/Email Input',
  component: EmailInput,
  tags: ['autodocs'],
};

export default meta;

export const Default: StoryObj<EmailInput> = {};

import { Meta, StoryObj } from '@storybook/angular';
import { PasswordInput } from '../../../app/components/common/password-input/password-input';

const meta: Meta<PasswordInput> = {
  title: 'Common/Password Input',
  component: PasswordInput,
  tags: ['autodocs'],
};

export default meta;

export const Default: StoryObj<PasswordInput> = {};

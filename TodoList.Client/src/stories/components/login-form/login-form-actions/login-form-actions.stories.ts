import { Meta, StoryObj } from '@storybook/angular';
import { LoginFormActions } from '../../../../app/components/login-form/login-form-actions/login-form-actions';

const meta: Meta<LoginFormActions> = {
  title: 'Login Form/Login Form Actions',
  component: LoginFormActions,
  tags: ['autodocs'],
  argTypes: {
    login: {
      action: 'login clicked'
    },
    register: {
      action: 'register clicked'
    },
  },
};

export default meta;

export const Default: StoryObj<LoginFormActions> = {};

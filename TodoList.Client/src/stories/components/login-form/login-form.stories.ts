import { Meta, StoryObj } from '@storybook/angular';
import { LoginForm } from '../../../app/components/login-form/login-form';

const meta: Meta<LoginForm> = {
  title: 'Login Form/Login Form',
  component: LoginForm,
  tags: ['autodocs'],
};

export default meta;

export const Default: StoryObj<LoginForm> = {};

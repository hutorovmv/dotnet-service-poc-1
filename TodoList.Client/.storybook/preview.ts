import type { Preview } from '@storybook/angular'
import { setCompodocJson } from '@storybook/addon-docs/angular';
import docJson from '../documentation.json';

setCompodocJson(docJson);

const preview: Preview = {
  parameters: {
    controls: {
      matchers: {
       color: /(background|color)$/i,
       date: /Date$/i,
      },
    },
  },
  decorators: [
    toggleDarkMode,
  ],
};

export default preview;

function toggleDarkMode(storyFn: any) {
  const toggleId = 'toggle-theme';
  const darkModeClass = 'dark-mode';

  if (!document.getElementById(toggleId)) {
    createToggleButton();
  }
  toggleBackground();

  function toggleBackground() {
    const lightBackground = '#ebdfea';
    const darkBackground = '#212121';

    if (document.body.classList.contains(darkModeClass)) {
      document.body.style.background = darkBackground;
    } else {
      document.body.style.background = lightBackground;
    }
  }

  function createToggleButton() {
    const btn = document.createElement('button');

    btn.id = toggleId;
    btn.innerText = 'Toggle Dark Mode';
    btn.style.position = 'fixed';
    btn.style.bottom = '16px';
    btn.style.right = '16px';
    btn.style.zIndex = '9999';
    btn.onclick = () => {
      document.body.classList.toggle(darkModeClass);
      toggleBackground();
    };

    document.body.appendChild(btn);
  }

  return storyFn();
}

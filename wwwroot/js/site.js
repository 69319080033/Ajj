document.querySelectorAll('[data-web-url]').forEach((input) => {
  input.addEventListener('input', () => input.setCustomValidity(''));
  input.form?.addEventListener('submit', () => {
    const value = input.value.trim();
    try {
      const url = new URL(value);
      const valid = (url.protocol === 'http:' || url.protocol === 'https:') && url.hostname.length > 0;
      input.setCustomValidity(valid ? '' : 'กรุณาใส่ URL แบบเต็มที่เป็น http หรือ https และมีชื่อโฮสต์');
    } catch {
      input.setCustomValidity('กรุณาใส่ URL แบบเต็มที่เป็น http หรือ https และมีชื่อโฮสต์');
    }
  });
});

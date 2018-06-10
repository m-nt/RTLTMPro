﻿using System.Linq;
using TMPro;
using UnityEngine;

namespace RTLTMPro
{
    [ExecuteInEditMode]
    public class RTLTextMeshPro : TextMeshProUGUI
    {
        // ReSharper disable once InconsistentNaming
        public override string text
        {
            get { return base.text; }
            set
            {
                originalText = value;
                havePropertiesChanged = true;
            }
        }

        public virtual bool PreserveNumbers
        {
            get { return preserveNumbers; }
            set
            {
                preserveNumbers = value;
                havePropertiesChanged = true;
            }
        }

        public bool Farsi
        {
            get { return farsi; }
            set
            {
                farsi = value;
                havePropertiesChanged = true;
            }
        }

        public bool PreserveTashkeel
        {
            get { return preserveTashkeel; }
            set
            {
                preserveTashkeel = value;
                havePropertiesChanged = true;
            }
        }

        public bool FixTags
        {
            get { return fixTags; }
            set
            {
                fixTags = value;
                havePropertiesChanged = true;
            }
        }

        [SerializeField] protected bool preserveNumbers;
        [SerializeField] protected bool farsi = true;
        [SerializeField] protected bool preserveTashkeel;
        [SerializeField] protected string originalText;
        [SerializeField] protected bool fixTags;

        protected RTLSupport support;

        protected override void OnEnable()
        {
            base.OnEnable();

            support = new RTLSupport();
            UpdateSupport();
        }

        protected virtual void Update()
        {
            if (havePropertiesChanged)
            {
                UpdateSupport();
                base.text = GetFixedText(originalText);
            }
        }

        protected virtual void UpdateSupport()
        {
            support.Farsi = farsi;
            support.PreserveNumbers = preserveNumbers;
            support.PreserveTashkeel = preserveTashkeel;
            support.FixTags = fixTags;
        }

        public virtual string GetFixedText(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = support.FixRTL(input);
            input = input.Reverse().ToArray().ArrayToString();
            isRightToLeftText = true;
            
            return input;
        }
    }
}
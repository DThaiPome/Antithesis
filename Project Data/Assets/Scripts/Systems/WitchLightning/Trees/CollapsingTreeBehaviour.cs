using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingTreeBehaviour : ACollapsibleTree
{
    private struct RendererToHighlight
    {
        public Renderer renderer;
        public Color baseColor;

        public RendererToHighlight(Renderer renderer, Color baseColor)
        {
            this.renderer = renderer;
            this.baseColor = baseColor;
        }
    }

    [SerializeField]
    private float fallSpeed = 180;
    [SerializeField]
    private Color highlightedColor;
    [SerializeField]
    private List<Renderer> renderersToHighlight;
    private List<RendererToHighlight> renderers;

    //For testing
    [SerializeField]
    private bool startHighlighted = false;
    [SerializeField]
    private bool knockDownAtStart = false;

    private bool isKnockingDown;
    private bool isKnockedDown;

    // Start is called before the first frame update
    void Start()
    {
        this.isKnockingDown = false;
        this.isKnockedDown = false;

        this.renderers = this.GetRendererPairs();

        if (this.knockDownAtStart)
        {
            this.KnockDown();
        }
        this.SetHighlighted(this.startHighlighted);
    }

    private List<RendererToHighlight> GetRendererPairs()
    {
        List<RendererToHighlight> result = new List<RendererToHighlight>();
        foreach(Renderer r in this.renderersToHighlight)
        {
            result.Add(new RendererToHighlight(r, r.material.color));
        }
        return result;
    }

    public override void KnockDown()
    {
        StartCoroutine(KnockDownTree());
    }

    /// <summary>
    /// Knock the tree down forwards.
    /// </summary>
    /// <returns></returns>
    private IEnumerator KnockDownTree()
    {
        this.isKnockingDown = true;

        float angle = 0;
        Quaternion baseRotation = this.transform.rotation;
        Vector3 axis = Vector3.right;
        while (angle < 90)
        {
            angle += this.fallSpeed * Time.deltaTime;
            this.transform.rotation = baseRotation * Quaternion.AngleAxis(angle, axis);
            yield return null;
        }

        this.transform.rotation = baseRotation * Quaternion.AngleAxis(90, axis);
        this.isKnockingDown = false;
        this.isKnockedDown = true;
    }

    public override void SetHighlighted(bool isHighlighted)
    {
        foreach (RendererToHighlight r in this.renderers)
        {
            if (isHighlighted)
            {
                r.renderer.material.color = GetUniColor(r.baseColor, this.highlightedColor);
            } else
            {
                r.renderer.material.color = r.baseColor;
            }
        }
    }

    /// <summary>
    /// Returns a new color that is a shade of the overlay color, with a brightness determined by the maximum
    /// color value in the base color.
    /// </summary>
    /// <param name="baseColor">the original color.</param>
    /// <param name="overlayColor">the new color at its maximum brightness.</param>
    /// <returns>a shade of the overlay color, blended using the overlay color's alpha value.</returns>
    private static Color GetUniColor(Color baseColor, Color overlayColor)
    {
        float mag = Mathf.Max(baseColor.r + baseColor.g + baseColor.b);
        Color newColor = new Color(
            overlayColor.r * mag,
            overlayColor.g * mag,
            overlayColor.b * mag,
            baseColor.a);
        return Color.Lerp(baseColor, newColor, overlayColor.a);
    }

    public override bool IsKnockedDown()
    {
        return this.PIsKnockedDown();
    }

    private bool PIsKnockedDown()
    {
        return this.isKnockedDown;
    }
}

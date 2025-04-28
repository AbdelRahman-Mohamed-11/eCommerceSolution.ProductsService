using Scalar.AspNetCore;

namespace ProductsMicroService.API.Extensions;

public static class ScalarExtensions
{
    public static void MapScalarDocs(this WebApplication app)
    {
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("Products MicroService API Documentation")
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Http2)
                .WithTheme(ScalarTheme.Purple)
                .WithCustomCss(@"
                    :root {
                        --scalar-background-1: #1a1c2c;
                        --scalar-background-2: #2a2c3e;
                        --scalar-background-3: #3a3c4e;
                        --scalar-color-1: #e0e0e0;
                        --scalar-color-2: #b0b0b0;
                        --scalar-color-3: #808080;
                        --scalar-color-accent: #5e81ac;
                        --scalar-font: 'Inter', -apple-system, BlinkMacSystemFont, sans-serif;
                        --scalar-sidebar-background-1: #2a2c3e;
                        --scalar-sidebar-color-1: #e0e0e0;
                        --scalar-sidebar-color-active: #5e81ac;
                        --scalar-sidebar-item-hover: #3a3c4e;
                        --scalar-button-1: #5e81ac;
                        --scalar-button-1-hover: #81a1c1;
                        --scalar-radius: 8px;
                    }
                    body {
                        font-family: var(--scalar-font);
                        background: linear-gradient(135deg, var(--scalar-background-1), var(--scalar-background-2));
                    }
                    .scalar-card {
                        border-radius: var(--scalar-radius);
                        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
                        transition: transform 0.2s ease-in-out;
                    }
                    .scalar-card:hover {
                        transform: translateY(-4px);
                    }
                    .t-tab-button {
                        border-radius: var(--scalar-radius);
                        padding: 8px 16px;
                        background: var(--scalar-button-1);
                        color: #fff;
                    }
                    .t-tab-button:hover {
                        background: var(--scalar-button-1-hover);
                    }
                    .scalar-sidebar {
                        border-right: 1px solid var(--scalar-background-3);
                    }
                    .scalar-logo {
                        padding: 20px;
                        background: var(--scalar-background-1);
                    }
                    h1, h2, h3 {
                        color: var(--scalar-color-accent);
                    }
                ")
                .WithSidebar(true);
        });
    }
}

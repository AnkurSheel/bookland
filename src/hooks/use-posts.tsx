import { graphql, useStaticQuery } from 'gatsby';
import { FluidObject } from 'gatsby-image';

export interface PostProps {
    allMdx: {
        nodes: {
            frontmatter: {
                title: string;
                slug: string;
                author: string;
                fluidImage: FluidObject;
            };
            excerpt: string;
        }[];
    };
}

const usePosts = () => {
    const data: any = useStaticQuery(graphql`
        query {
            allMdx {
                nodes {
                    frontmatter {
                        author
                        slug
                        title
                        image {
                            sharp: childImageSharp {
                                fluid(
                                    maxWidth: 100
                                    maxHeight: 100
                                    duotone: { shadow: "#663399", highlight: "#ddbbff" }
                                ) {
                                    ...GatsbyImageSharpFluid_withWebp
                                }
                            }
                        }
                    }
                }
            }
        }
    `);

    return data.allMdx.nodes.map((post: any) => ({
        title: post.frontmatter.title,
        author: post.frontmatter.author,
        slug: post.frontmatter.slug,
        image: post.frontmatter.image,
        excerpt: post.excerpt,
    }));
};

export default usePosts;
